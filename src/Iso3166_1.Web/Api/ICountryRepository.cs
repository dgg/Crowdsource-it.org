using System.Collections.Generic;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Models;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public interface ICountryRepository
	{
		IEnumerable<Country> FindCurrent(CultureInfo language);
		Country Get(string alpha2_Code, CultureInfo language);
		IEnumerable<Country> FindAll(CultureInfo language);
	}
}