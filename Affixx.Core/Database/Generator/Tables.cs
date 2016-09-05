using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Affixx.Core.Database.Generator
{
	public class Tables : List<Table>
	{
		public Table GetTable(string tableName)
		{
			return this.Single(x => string.Compare(x.Name, tableName, true, CultureInfo.InvariantCulture) == 0);
		}

		public Table this[string tableName]
		{
			get { return GetTable(tableName); }
		}

		public static Tables Load(string connectionString, string schemaName, bool includeViews)
		{
			var result = ReadTables(connectionString, schemaName, includeViews);

			var cleanupRegex = new Regex("^(Equals|GetHashCode|GetType|ToString|repo|Save|IsNew|Insert|Update|Delete|Exists|SingleOrDefault|Single|First|FirstOrDefault|Fetch|Page|Query)$");
			foreach (var table in result) {
				foreach (var column in table.Columns) {
					column.PropertyName = cleanupRegex.Replace(column.PropertyName, "_$1");

					// Make sure property name doesn't clash with class name
					if (column.PropertyName == table.ClassName)
						column.PropertyName = "_" + column.PropertyName;
				}
			}

			result.Sort((x, y) => string.Compare(x.Name, y.Name, System.StringComparison.Ordinal));
			return result;
		}

		private static Tables ReadTables(string connectionString, string schemaName, bool includeViews)
		{
			Tables result;
			using (var connection = new SqlConnection(connectionString)) {
				connection.Open();
				result = new SqlServerSchemaReader().ReadSchema(connection);

				FilterTables(result, schemaName, includeViews);
			}

			return result;
		}

		private static void FilterTables(Tables result, string schemaName, bool includeViews)
		{
			// Remove some tables and views if necessary
			for (int i = result.Count - 1; i >= 0; i--) {
				if (schemaName != null && string.Compare(result[i].Schema, schemaName, System.StringComparison.OrdinalIgnoreCase) != 0) {
					result.RemoveAt(i);
				}
				else if (!includeViews && result[i].IsView) {
					result.RemoveAt(i);
				}
			}
		}
	}
}