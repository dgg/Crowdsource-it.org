using ServiceStack.ServiceInterface.ServiceModel;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	public class TranslationResponse : IHasResponseStatus
	{
		public bool Sucess { get; set; }
		public string Uri { get; set; }
		public ResponseStatus ResponseStatus { get; set; }
	}
}