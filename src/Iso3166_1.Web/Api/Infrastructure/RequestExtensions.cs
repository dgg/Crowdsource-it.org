using System.Globalization;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public static class RequestExtensions
	{
		public static CultureInfo Language(this IRequestContext request, string languageInMessage)
		{
			CultureInfo language;
			var available = new AvailableLanguages();
			if (string.IsNullOrEmpty(languageInMessage) || !available.ContainsKey(languageInMessage))
			{
				string header = request.GetHeader("Accept-Language");
				
				language = available.GetOrInvariant(header);
			}
			else
			{
				language = available[languageInMessage];
			}
			return language;
		}
	}
}