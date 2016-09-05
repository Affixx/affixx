using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affixx.Core.Logging
{
	public interface ILogger
	{
		string Name { get; }
		void Debug(string message);
		void Info(string message);
		void Warn(string message);
		void Error(string message, Exception ex = null);
	}
}