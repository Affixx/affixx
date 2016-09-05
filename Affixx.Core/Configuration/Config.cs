using System.Configuration;
using System.IO;
using System.Reflection;

namespace Affixx.Core
{
	public static class Config
	{
		private static readonly IConfig _default = new DefaultConfig();
		private static IConfig _current = _default;

		public static IConfig Default
		{
			get { return _default; }
		}

		public static IConfig Current
		{
			get { return _current; }
			set { _current = value; }
		}

		private class DefaultConfig : IConfig
		{
			private static readonly Configuration _configuration;
            private static readonly string _cloudSecretFile;
            private static readonly string _cloudCredentialsFolder;

            static DefaultConfig()
			{
                var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.Replace("file:///", ""));
                var file = Path.Combine(folder, "Configuration", "Core.config");
                var map = new ExeConfigurationFileMap { ExeConfigFilename = file };
				_configuration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                _cloudSecretFile = Path.Combine(folder, "Configuration", "googledrive.secret");
                _cloudCredentialsFolder = Path.Combine(folder, "Configuration");
            }

			public string ConnectionString { get { return GetAppSettingsValue("database"); } }
			public string Environment { get { return GetAppSettingsValue("environment"); } }
            public string CloudSecretFile { get { return _cloudSecretFile; } }
            public string CloudCredentialsFolder { get { return _cloudCredentialsFolder; } }

            private string GetAppSettingsValue(string key)
            {
                return _configuration.AppSettings.Settings[key].Value;
            }            
        }
	}
}
