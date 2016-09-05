using System;

namespace Affixx.Core.Logging
{
	internal class Logger : ILogger
	{
		private readonly NLog.Logger _logger;
		public string Name { get { return _logger.Name; } }

		internal Logger(NLog.Logger logger)
		{
			_logger = logger;
		}

		public void Debug(string message)
		{
			_logger.Debug(message);
		}

		public void Info(string message)
		{
			_logger.Info(message);
		}

		public void Warn(string message)
		{
			_logger.Warn(message);
		}

		public void Error(string message, Exception ex = null)
		{
			_logger.Error(message);
			if (ex != null) {
				_logger.Error(ex);
			}
		}
	}
}
