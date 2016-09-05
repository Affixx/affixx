using System.Collections.Generic;

namespace Affixx.Core.Database.Generator
{
	public class ForeignKey
	{
		public ForeignKey()
		{
			this.ForeignColumns = new List<string>();
			this.PrimaryColumns = new List<string>();
		}

		public string Name { get; set; }

		public string PrimaryTable { get; set; }
		public string PrimaryTableSchema { get; set; }
		public ICollection<string> PrimaryColumns { get; set; }

		public string ForeignTable { get; set; }
		public string ForeignTableSchema { get; set; }
		public ICollection<string> ForeignColumns { get; set; }

		public string PrimaryClass { get; set; }

		public System.Data.Rule UpdateRule { get; set; }
		public System.Data.Rule DeleteRule { get; set; }
	}
}