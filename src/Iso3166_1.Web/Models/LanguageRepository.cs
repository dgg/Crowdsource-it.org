using System.Collections.Generic;
using System.Data;
using Iso3166_1.Crowdsource_it.org.Web.Api;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	internal class LanguageRepository : ILanguageRepository
	{
		private readonly IDbConnection _db;

		public LanguageRepository(IDbConnection db)
		{
			_db = db;
		}

		public IEnumerable<string> FindAll()
		{
			using (_db)
			{
				_db.Open();

				IEnumerable<string> model = _db.AvailableLanguages();
				return model;
			}
		}
	}
}