using System.Linq;
using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using Iso3166_1.Crowdsource_it.org.Web.Api.Resources;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class LanguageService : IService<Languages>
	{
		public ILanguageRepository Repository { get; set; }

		public object Execute(Languages request)
		{
			return new LanguagesResponse
			{
				Languages = Repository.FindAll()
					.Select(l => new Language { Code = l })
					.ToArray()
			};
		}
	}
}