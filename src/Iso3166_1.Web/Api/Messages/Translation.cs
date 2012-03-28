using System;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	[RestService(Route.Extended, "GET, HEAD, DELETE, PUT")]
	[RestService(Route.Base, "POST")]
	public class Translation
	{
		public string Code { get; set; }
		public string Language { get; set; }
		public string Data { get; set; }

		internal static class Route
		{
			internal const string Base = "/translation";
			internal const string Extended = Base + "/{code}/{language}";

			public static Uri Write(Uri baseUrl, string code, string language)
			{
				string path = string.Join("/", new[] {Base, code, language});
				return new Uri(baseUrl, new Uri(path, UriKind.Relative));
			}
		}

		public Models.Translation ToModel()
		{
			return new Models.Translation
			{
				Alpha2 = Code,
				Language = Language,
				Name = Data
			};
		}
	}
}