using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Affixx.Core.Database.Generator
{
	public class Table
	{
		public List<Column> Columns;
		public string Name;
		public string Schema;
		public bool IsView;
		public string CleanName;
		public string ClassName;
		public string SequenceName;
		public bool Ignore;

		public Column PK
		{
			get { return Columns.SingleOrDefault(x => x.IsPK); }
		}

		public Column GetColumn(string columnName)
		{
			return Columns.Single(x => string.Compare(x.Name, columnName, StringComparison.OrdinalIgnoreCase) == 0);
		}

		public Column this[string columnName]
		{
			get { return GetColumn(columnName); }
		}

		public string ColumnNames
		{
			get { return string.Join(", ", AllowedColumns.Select(x => "[" + x.Name + "]")); }
		}

		private List<string> _columnValues;
		public List<string> ColumnValues
		{
			get
			{
				if (_columnValues == null) {
					_columnValues = AllowedColumns.Select(x => {
						return "@" + x.Name;
					}).ToList();
				}
				return _columnValues;
			}
		}

		public string UpdateSql
		{
			get
			{
				var sb = new StringBuilder();
				foreach (var column in AllowedColumns) {
					sb.AppendFormat("[{0}] = @{0}, ", column.Name);
				}
				return sb.Remove(sb.Length - 2, 1) + string.Format(" WHERE [{0}] = {1}", PK.Name, (PK.PropertyType == "string") ? string.Format("N'\"+{0}+\"'", PK.Name) : "@" + PK.Name);
			}
		}

		private IEnumerable<Column> AllowedColumns
		{
			get { return Columns.Where(x => !(x.IsPK && x.IsAutoIncrement) && !x.Ignore).OrderBy(x => x.Name); }
		}
	}

}