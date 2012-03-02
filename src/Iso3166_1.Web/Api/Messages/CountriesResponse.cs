using ServiceStack.ServiceInterface.ServiceModel;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	public class CountriesResponse : IHasResponseStatus
	{
		public Resources.Country[] Countries { get; set; }
		public ResponseStatus ResponseStatus { get; set; }
	}
}