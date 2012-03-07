using Funq;
using Iso3166_1.Crowdsource_it.org.Web.App_Start;
using ServiceStack.WebHost.Endpoints;

namespace Iso3166_1.Tests.Api
{
	public class TestHost : AppHostHttpListenerBase
	{
		public TestHost() : base(HostBootstrapper.ServiceName, HostBootstrapper.ServiceContainer) { }

		public override void Configure(Container container)
		{
			var bootstrapper = new HostBootstrapper();
			
			EndpointHostConfig config = bootstrapper
				.Bootstrap(Routes)
				.Bootstrap(container)
				.Bootstrap(RequestFilters, ResponseFilters)
				.WithConfig();

			SetConfig(config);
		}
	}
}
