using System;
using System.Web;
using Elmah;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class ElmahVerboserLogger : ILog
	{
		private readonly ElmahInterceptingLogger _decoree;

		public ElmahVerboserLogger(ElmahInterceptingLogger decoree)
		{
			_decoree = decoree;
		}

		public void Debug(object message, Exception exception)
		{
			_decoree.Debug(message, exception);
		}

		public void Debug(object message)
		{
			_decoree.Debug(message);
		}

		public void DebugFormat(string format, params object[] args)
		{
			_decoree.DebugFormat(format, args);
		}

		public void Error(object message, Exception exception)
		{
			_decoree.Error(message, exception);
		}

		public void Error(object message)
		{
			_decoree.Error(message);
		}

		public void ErrorFormat(string format, params object[] args)
		{
			_decoree.ErrorFormat(format, args);
		}

		public void Fatal(object message, Exception exception)
		{
			_decoree.Fatal(message, exception);
		}

		public void Fatal(object message)
		{
			_decoree.Fatal(message);
		}

		public void FatalFormat(string format, params object[] args)
		{
			_decoree.FatalFormat(format, args);
		}

		public void Info(object message, Exception exception)
		{
			_decoree.Info(message, exception);
		}

		public void Info(object message)
		{
			ErrorLog.GetDefault(HttpContext.Current).Log(new Error(new Elmah.ApplicationException(safeToString(message)), HttpContext.Current));
			_decoree.Info(message);
		}

		public void InfoFormat(string format, params object[] args)
		{
			_decoree.InfoFormat(format, args);
		}

		private string safeToString(object obj)
		{
			return obj == null ? string.Empty : obj.ToString();
		}

		public void Warn(object message, Exception exception)
		{
			ErrorLog.GetDefault(HttpContext.Current).Log(new Error(exception, HttpContext.Current));
			_decoree.Warn(message, exception);
		}

		public void Warn(object message)
		{
			_decoree.Warn(message);
		}

		public void WarnFormat(string format, params object[] args)
		{
			_decoree.WarnFormat(format, args);
		}

		public bool IsDebugEnabled
		{
			get { return _decoree.IsDebugEnabled; }
		}
	}
}