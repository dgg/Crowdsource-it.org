using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	// the route is defined in the message
	[RestService("/country/{code}", "GET, HEAD")]
	public class Country
	{
		public string Code { get; set; }
	}
}