namespace Affixx.Core.Database.Generator
{
	public class Column
	{
		public string Name;
		public string PropertyName;
		public string PropertyType;
		public bool IsPK;
		public bool IsNullable;
		public bool IsAutoIncrement;
		public bool Ignore;

		public string NullableSign()
		{
			string result = "";
			if (IsNullable && 
				PropertyType != "byte[]" &&
				PropertyType != "Byte[]" &&
				PropertyType != "string" &&
				PropertyType != "String"
				)
				result = "?";
			return result;
		}

	}
}