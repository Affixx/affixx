using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Affixx.Core.Database.Generator
{
	internal class SqlServerSchemaReader : SchemaReader
	{
		private DbConnection _connection;
		
		public override Tables ReadSchema(DbConnection connection)
		{
			var result = new Tables();

			_connection = connection;

			using (var command = _connection.CreateCommand()) {
				command.CommandText = TABLE_SQL;

				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var table = new Table();
						table.Name = reader["TABLE_NAME"].ToString();
						table.Schema = reader["TABLE_SCHEMA"].ToString();
						table.IsView = string.Compare(reader["TABLE_TYPE"].ToString(), "View", true) == 0;
						table.CleanName = CleanUp(table.Name);
						table.ClassName = Inflector.MakeSingular(table.CleanName);

						result.Add(table);
					}
				}
			}

			//pull the tables in a reader

			foreach (var table in result) {
				table.Columns = LoadColumns(table);

				// Mark the primary key
				string primaryKey = GetPK(table.Name);
				var pkColumn = table.Columns.SingleOrDefault(x => x.Name.ToLower().Trim() == primaryKey.ToLower().Trim());
				if (pkColumn != null) {
					pkColumn.IsPK = true;
				}
			}

			return result;
		}

		private List<Column> LoadColumns(Table tbl)
		{
			using (var command = _connection.CreateCommand()) {
				command.Connection = _connection;
				command.CommandText = COLUMN_SQL;

				var p = command.CreateParameter();
				p.ParameterName = "@tableName";
				p.Value = tbl.Name;
				command.Parameters.Add(p);

				p = command.CreateParameter();
				p.ParameterName = "@schemaName";
				p.Value = tbl.Schema;
				command.Parameters.Add(p);

				var result = new List<Column>();
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var column = new Column();
						column.Name = reader["ColumnName"].ToString();
						column.PropertyName = CleanUp(column.Name);
						column.PropertyType = GetPropertyType(reader["DataType"].ToString());
						column.IsNullable = reader["IsNullable"].ToString() == "YES";
						column.IsAutoIncrement = ((int)reader["IsIdentity"]) == 1;
						result.Add(column);
					}
				}

				return result;
			}
		}

		string GetPK(string table)
		{

			string sql = @"
SELECT c.name AS ColumnName
FROM sys.indexes AS i 
INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id 
INNER JOIN sys.objects AS o ON i.object_id = o.object_id 
LEFT OUTER JOIN sys.columns AS c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
WHERE (i.type = 1) AND (o.name = @tableName)
";
			using (var command = _connection.CreateCommand()) {
				command.CommandText = sql;

				var p = command.CreateParameter();
				p.ParameterName = "@tableName";
				p.Value = table;
				command.Parameters.Add(p);

				var result = command.ExecuteScalar();

				if (result != null)
					return result.ToString();
			}

			return "";
		}

		string GetPropertyType(string sqlType)
		{
			string sysType = "string";
			switch (sqlType)
			{
				case "bigint":
					sysType = "long";
					break;
				case "smallint":
					sysType = "short";
					break;
				case "int":
					sysType = "int";
					break;
				case "uniqueidentifier":
					sysType = "Guid";
					break;
				case "smalldatetime":
				case "datetime":
				case "date":
				case "time":
					sysType = "DateTime";
					break;
				case "float":
					sysType = "double";
					break;
				case "real":
					sysType = "float";
					break;
				case "numeric":
				case "smallmoney":
				case "decimal":
				case "money":
					sysType = "decimal";
					break;
				case "tinyint":
					sysType = "byte";
					break;
				case "bit":
					sysType = "bool";
					break;
				case "image":
				case "binary":
				case "varbinary":
				case "timestamp":
					sysType = "byte[]";
					break;
				case "geography":
					sysType = "Microsoft.SqlServer.Types.SqlGeography";
					break;
				case "geometry":
					sysType = "Microsoft.SqlServer.Types.SqlGeometry";
					break;
			}
			return sysType;
		}

		const string TABLE_SQL = @"
SELECT *
FROM  INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE='BASE TABLE' OR TABLE_TYPE='VIEW'
";

		const string COLUMN_SQL = @"
SELECT 
	TABLE_CATALOG AS [Database],
	TABLE_SCHEMA AS Owner, 
	TABLE_NAME AS TableName, 
	COLUMN_NAME AS ColumnName, 
	ORDINAL_POSITION AS OrdinalPosition, 
	COLUMN_DEFAULT AS DefaultSetting, 
	IS_NULLABLE AS IsNullable, DATA_TYPE AS DataType, 
	CHARACTER_MAXIMUM_LENGTH AS MaxLength, 
	DATETIME_PRECISION AS DatePrecision,
	COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsIdentity') AS IsIdentity,
	COLUMNPROPERTY(object_id('[' + TABLE_SCHEMA + '].[' + TABLE_NAME + ']'), COLUMN_NAME, 'IsComputed') as IsComputed
FROM  INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME=@tableName AND TABLE_SCHEMA=@schemaName
ORDER BY OrdinalPosition ASC
";
	}
}