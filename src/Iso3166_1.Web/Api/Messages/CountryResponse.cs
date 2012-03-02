using ServiceStack.ServiceInterface.ServiceModel;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	public class CountryResponse : IHasResponseStatus
	{
		public Resources.Country Country { get; set; }
		public ResponseStatus ResponseStatus { get; set; }
	}
}