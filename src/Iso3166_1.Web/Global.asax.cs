using System;
using System.Web.Mvc;
using System.Web.Routing;
using ServiceStack.MiniProfiler;

namespace Iso3166_1.Crowdsource_it.org.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("api/{*pathInfo}"); 
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (Request.IsLocal) Profiler.Start();
		}

		protected  void Application_EndRequest(object sender, EventArgs e)
		{
			Profiler.Stop();
		}
	}
}