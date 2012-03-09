using System.Collections.Generic;
using System.Linq;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class LanguageService : IService<Messages.Languages>
	{
		public ILanguageRepository Repository { get; set; }

		public object Execute(Messages.Languages request)
		{
			IEnumerable<string> model = Repository.FindAll();

			return model.ToResponse(()=>new Messages.LanguagesResponse
			{
				Languages = model.Select(l => new Resources.Language { Code = l}).ToArray()
			});
		}
	}
}