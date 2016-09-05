using System.Collections.Generic;
using Affixx.Core.Database.Generated;

namespace Affixx.Core.Database
{
	public interface IRepository<TEntity>
		where TEntity : TableEntity
	{
		IEnumerable<TEntity> Select(string sqlWhere = null, dynamic param = null, int timeoutSeconds = 15, bool bypassCache = false);
		bool Save(TEntity item);
	}
}
