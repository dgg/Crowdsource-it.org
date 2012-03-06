using ServiceStack.ServiceInterface.ServiceModel;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	public class LanguagesResponse : IHasResponseStatus
	{
		public Resources.Language[] Languages { get; set; }
		public ResponseStatus ResponseStatus { get; set; }
	}
}