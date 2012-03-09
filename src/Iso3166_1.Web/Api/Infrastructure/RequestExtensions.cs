using System.Globalization;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public static class RequestExtensions
	{
		public static CultureInfo Language(this IRequestContext request, string languageInMessage)
		{
			CultureInfo language;
			if (string.IsNullOrEmpty(languageInMessage) || !Available.Languages.ContainsKey(languageInMessage))
			{
				string header = request.GetHeader("Accept-Language");
				
				language = Available.Languages.GetOrInvariant(header);
			}
			else
			{
				language = Available.Languages[languageInMessage];
			}
			return language;
		}
	}
}