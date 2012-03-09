using System.Web;
using System.Web.Mvc;
using ServiceStack.Logging;
using ServiceStack.Mvc;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Iso3166_1.Crowdsource_it.org.Web.App_Start.AppHost), "Start")]

namespace Iso3166_1.Crowdsource_it.org.Web.App_Start
{
	public class AppHost : AppHostBase
	{
		public AppHost() : base(HostBootstrapper.ServiceName, HostBootstrapper.ServiceContainer) { }

		public override void Configure(Funq.Container container)
		{
			var bootstrapper = new HostBootstrapper();
			
			EndpointHostConfig config = bootstrapper
				.Bootstrap(Routes)
				.Bootstrap(container)
				.Bootstrap(RequestFilters, ResponseFilters)
				.WithConfig();

			SetConfig(config);

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}