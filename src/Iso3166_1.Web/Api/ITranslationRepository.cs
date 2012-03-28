using System;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Models;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public interface ITranslationRepository
	{
		bool Exists(string alpha2_Code, CultureInfo language);
		void Create(Translation translation);
		bool Update(Translation translation);
	}
}