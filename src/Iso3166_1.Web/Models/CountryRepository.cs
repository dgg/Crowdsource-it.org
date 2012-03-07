using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Api;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	internal class CountryRepository : ICountryRepository
	{
		private readonly IDbConnection _db;

		public CountryRepository( IDbConnection db)
		{
			_db = db;
		}

		public Country Get(string alpha2_Code, CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				
				Country model = _db.Country(alpha2_Code, language.NeutralName());
				return model;
			}
		}

		public IEnumerable<Country> FindCurrent(CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				IEnumerable<Country> model = _db.CurrentCountries(language);
				return model;
			}
		}

		public IEnumerable<Country> FindAll(CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				IEnumerable<Country> model = _db.AllCountries(language.NeutralName());
				return model;
			}
		}
	}
}