using System;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	internal class ElmahVerboserLoggerFactory : ILogFactory
	{
		private readonly ElmahLogFactory _decoree;

		public ElmahVerboserLoggerFactory(ElmahLogFactory decoree)
		{
			_decoree = decoree;
		}

		public ILog GetLogger(Type type)
		{

			return new ElmahVerboserLogger(_decoree.GetLogger(type) as ElmahInterceptingLogger);
		}

		public ILog GetLogger(string typeName)
		{
			return new ElmahVerboserLogger(_decoree.GetLogger(typeName) as ElmahInterceptingLogger);
		}
	}
}