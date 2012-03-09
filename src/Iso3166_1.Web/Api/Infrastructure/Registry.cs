using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using Funq;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;
using ServiceStack.Logging.Support.Logging;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class Registry
	{
		public Registry Register(Container container)
		{
			container.Register<ICacheClient>(new MemoryCacheClient());

			container.Register<IDbConnection>(c =>
				new SqlCeConnection(ConfigurationManager.ConnectionStrings["Iso3166_1"].ConnectionString))
				.ReusedWithin(ReuseScope.None);

			container.RegisterAutoWiredAs<CountryRepository, ICountryRepository>()
				.ReusedWithin(ReuseScope.None);
			container.RegisterAutoWiredAs<LanguageRepository, ILanguageRepository>()
				.ReusedWithin(ReuseScope.None);

			var logFactory = new ElmahVerboserLoggerFactory(new ElmahLogFactory(new NullLogFactory()));
			// allows ServiceStack to log its own error (for instance 404)
			LogManager.LogFactory = logFactory;

			// allow services to declare the ILogFactory dependency instead of depending on the global LogManager
			container.Register<ILogFactory>(logFactory);

			return this;
		}
	}
}