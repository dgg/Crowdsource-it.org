using System.Web.Mvc;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using ServiceStack.Mvc;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Iso3166_1.Crowdsource_it.org.Web.App_Start.AppHost), "Start")]

namespace Iso3166_1.Crowdsource_it.org.Web.App_Start
{
	public class AppHost : AppHostBase
	{		
		public AppHost() : base("ISO 3166-1 Crowdsource-it", typeof(CountryService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

			//Change the default ServiceStack configuration
			SetConfig(new EndpointHostConfig
			{
				DebugMode = true, //Show StackTraces in responses in development
			});

			new Registry().Register(container);

			//Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));

			Routes.Add<Api.Messages.Countries>("/countries", "GET");
		}

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}