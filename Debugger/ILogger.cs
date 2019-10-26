using System;

namespace Debugger
{
	public interface ILogger
	{
		void Log(string message, Level level = Level.Info, Exception exception = null);
	}
}