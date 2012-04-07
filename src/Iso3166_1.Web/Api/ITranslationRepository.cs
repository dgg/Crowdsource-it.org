using System;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Models;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public interface ITranslationRepository
	{
		Translation Get(string alpha2_Code, CultureInfo language);
		bool Create(Translation toBeCreated);
		bool Update(Translation toBeUpdated);
		bool Delete(Translation toBeDeleted);
	}
}