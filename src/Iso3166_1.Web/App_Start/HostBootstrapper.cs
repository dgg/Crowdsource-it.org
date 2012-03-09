using System;
using System.Collections.Generic;
using System.Reflection;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;

namespace Iso3166_1.Crowdsource_it.org.Web.App_Start
{
	public class HostBootstrapper
	{
		public static readonly string ServiceName = "ISO 3166-1 Crowdsource-it";
		public static readonly Assembly ServiceContainer = typeof (CountryService).Assembly;

		/// <summary>
		/// Sets service routes
		/// </summary>
		public HostBootstrapper Bootstrap(IServiceRoutes routes)
		{
			routes.Add<Api.Messages.Countries>("/countries/{language}", "GET");
			routes.Add<Api.Messages.Countries>("/countries", "GET");

			return this;
		}

		/// <summary>
		/// Terminates configuration by returning the configuration
		/// </summary>
		public EndpointHostConfig WithConfig()
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

			return new EndpointHostConfig
			{
				DebugMode = true, //Show StackTraces in responses in development
			};
		}

		public HostBootstrapper Bootstrap(Funq.Container container)
		{
			new Registry().Register(container);

			return this;
		}

		public HostBootstrapper Bootstrap(
			List<Action<IHttpRequest, IHttpResponse, object>> requestFilters,
			List<Action<IHttpRequest, IHttpResponse, object>> responseFilters)
		{
			responseFilters.Add(new NotFoundResourceFilter().ResponseFilter);

			return this;
		}
	}
}