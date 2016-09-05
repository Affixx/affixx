using System.Diagnostics;

namespace Affixx.Core.Logging
{
	public static class LogManager
	{
		public static ILogger GetLogger()
		{
			var nlogger = NLog.LogManager.GetLogger(GetLoggerName());
			return new Logger(nlogger);
		}

		/// <summary>
		/// Adopted from "GetCurrentClassLogger" of NLog.
		/// </summary>
		public static string GetLoggerName()
		{
			int framesToSkip = 2;
			var method = new StackFrame(framesToSkip, false).GetMethod();

			return (method.DeclaringType == null) ? method.Name : method.DeclaringType.FullName;
		}
	}
}
