namespace Affixx.Core.Database
{
	public static class Cache
	{
		private static ICache _currentCache = new Empty();

		public static ICache Current
		{
			get { return _currentCache; }
			set { _currentCache = value; }
		}

		private class Empty : ICache
		{
			public object Get(string key) { return null; }
			public void Add(string key, object value) { }
			public void Remove(string key) { }
		}
	}
}
