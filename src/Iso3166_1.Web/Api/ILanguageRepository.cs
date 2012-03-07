using System.Collections.Generic;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public interface ILanguageRepository
	{
		IEnumerable<string> FindAll();
	}
}