using System.Data;
using System.Linq;
using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using Iso3166_1.Crowdsource_it.org.Web.Api.Resources;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class LanguageService : IService<Languages>
	{
		public IDbConnection Db { get; set; }

		public object Execute(Languages request)
		{
			using (Db)
			{
				Db.Open();
				return new LanguagesResponse
				{
					Languages = Db.AvailableLanguages()
						.Select(l => new Language { Code = l })
						.ToArray()
				};
			}
		}
	}
}