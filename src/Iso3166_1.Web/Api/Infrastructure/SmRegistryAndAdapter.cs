using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.Elmah;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using StructureMap;
using fContainer = Funq.Container;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class SmRegistryAndAdapter : IContainerAdapter
	{
		public SmRegistryAndAdapter Register(fContainer container)
		{
			ObjectFactory.Configure(config =>
				{
					config.For<ICacheClient>().Use(new MemoryCacheClient());
					config.For<IDbConnection>()
					.Use(ctx => new ProfiledDbConnection(
					new SqlCeConnection(ConfigurationManager.ConnectionStrings["Iso3166_1"].ConnectionString),
					Profiler.Current));
					config.For<ICountryRepository>().Use<CountryRepository>();
					config.For<ILanguageRepository>().Use<LanguageRepository>();
					config.For<ITranslationRepository>().Use<TranslationRepository>();

					var logFactory = new ElmahVerboserLoggerFactory(new ElmahLogFactory(new NullLogFactory()));
					// allows ServiceStack to log its own error (for instance 404)
					LogManager.LogFactory = logFactory;

					// allow services to declare the ILogFactory dependency instead of depending on the global LogManager
					config.For<ILogFactory>().Use(logFactory);
				});

			container.Adapter = this;

			return this;
		}

		public T TryResolve<T>()
		{
			return ObjectFactory.TryGetInstance<T>();
		}

		public T Resolve<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}
	}
}