using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Affixx.Core.Database
{
	public class Repository
	{
		private readonly object _sync = new object();
		
		public IEnumerable<TEntity> SelectAdHoc<TEntity>(string query, object param = null, int timeoutSeconds = 15, bool bypassCache = false)
		{
			var cacheKey = query;
			return SelectFromDatabase<TEntity>(query, param, timeoutSeconds, bypassCache, cacheKey);
		}

		public int ExecuteAdHoc(string sql, object param = null, int timeoutSeconds = 15, bool bypassCache = false)
		{
			using (var connection = OpenConnection()) {
				var results = connection.Query<dynamic>(sql, param, commandTimeout: timeoutSeconds);
				return results.Count();
			}
		}

		protected IEnumerable<TEntity> SelectFromDatabase<TEntity>(string sqlSelect, object param, int timeoutSeconds, bool bypassCache, string cacheKey)
		{
			Func<IEnumerable<TEntity>> getFromDatabase = () => {
				using (var connection = OpenConnection()) {
					return connection.Query<TEntity>(sqlSelect, param, commandTimeout: timeoutSeconds);
				}
			};

			if (bypassCache) {
				return getFromDatabase();
			}

			var cache = Cache.Current;

			var result = cache.Get(cacheKey) as IEnumerable<TEntity>;
			if (result == null) {
				lock (_sync) {
					result = cache.Get(cacheKey) as IEnumerable<TEntity>;
					if (result == null) {
						result = getFromDatabase();
						cache.Add(cacheKey, result);
					}
				}
			}

            return result;
        }

		protected SqlConnection OpenConnection()
		{
			var connection = new SqlConnection(Config.Current.ConnectionString);
			connection.Open();

			return connection;
		}
	}
}
