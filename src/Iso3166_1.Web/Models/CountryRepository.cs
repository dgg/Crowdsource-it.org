using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using ServiceStack.CacheAccess;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	internal class CountryRepository : ICountryRepository
	{
		private readonly IDbConnection _db;
		private readonly ICacheClient _cache;

		public CountryRepository(IDbConnection db, ICacheClient cache)
		{
			_db = db;
			_cache = cache;
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
			using (_cache)
			{
				var hash = new CacheHash(language);
				var cached = _cache.Get<IEnumerable<Country>>(hash.Value);
				if (cached == null)
				{
					cached = findCurrent(language);
					_cache.Add(hash.Value, cached, TimeSpan.FromSeconds(10));
				}
				return cached;
			}
		}

		private IEnumerable<Country> findCurrent(CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				IEnumerable<Country> model = _db.CurrentCountries(language.NeutralName());
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

		public bool AreTranslated(CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				bool model = _db.CurrentExist(language.NeutralName());
				return model;
			}
		}

		public bool Exists(string alpha2_Code, CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				bool model = _db.CurrentExists(alpha2_Code, language.NeutralName());
				return model;
			}
		}
	}
}