using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Dapper;
using Affixx.Core.Database.Generated;
using Affixx.Core.Database.Generator;

namespace Affixx.Core.Database
{
	public class Repository<TEntity> : Repository, IRepository<TEntity>
		where TEntity : TableEntity
	{
		public IEnumerable<TEntity> Select(string sqlWhere = null, object param = null, int timeoutSeconds = 15, bool bypassCache = false)
		{
			var query = "SELECT * FROM [" + GetTableName() + "]" + (sqlWhere == null ? "" : " WHERE " + sqlWhere);
			var cacheKey = CreateCacheKey(query, param);

			return SelectFromDatabase<TEntity>(query, param, timeoutSeconds, bypassCache, cacheKey);
		}

		public bool Save(TEntity item)
		{
			using (var connection = OpenConnection()) {
				connection.EnlistTransaction(Transaction.Current);

				if (item.Id == 0) {
					var id = connection.Query<dynamic>(item.EntityInsertSql + "; SELECT CAST(SCOPE_IDENTITY() AS BIGINT) AS Id", item).Single().Id;
					item.EntitySetId(id);

					return (id > 0);
				}

				connection.Execute(item.EntityUpdateSql, item);
				return true;
			}
		}

		public void Delete(string where, object param = null)
		{
			var query = "DELETE FROM [" + GetTableName() + "] WHERE " + where;
			using (var connection = OpenConnection()) {
				connection.EnlistTransaction(Transaction.Current);
				connection.Execute(query, param);
			}
		}

		private string GetTableName()
		{
			return Inflector.MakePlural(typeof(TEntity).Name);
		}

		private string CreateCacheKey(string query, object param)
		{
			return query;
		}
	}
}
