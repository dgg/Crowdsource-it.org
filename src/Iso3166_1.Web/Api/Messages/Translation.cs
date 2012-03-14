using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	[RestService("/translation/{code}/{language}", "GET, HEAD, DELETE")]
	[RestService("/translation", "POST, PUT")]
	public class Translation
	{
		public string Code { get; set; }
		public string Language { get; set; }
		public Translation Data { get; set; }
	}
}