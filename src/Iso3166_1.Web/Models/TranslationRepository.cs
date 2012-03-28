using System.Data;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Api;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	internal class TranslationRepository : ITranslationRepository
	{
		private readonly IDbConnection _db;

		public TranslationRepository(IDbConnection db)
		{
			_db = db;
		}

		public bool Exists(string alpha2_Code, CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				bool model = _db.CurrentExists("Staged_Translations", alpha2_Code, language.NeutralName());
				return model;
			}
		}

		public void Create(Translation translation)
		{
			using (_db)
			{
				_db.Open();
				_db.Insert(translation.Alpha2, translation.Language, translation.Name);
			}
		}

		public bool Update(Translation translation)
		{
			bool updated;
			using (_db)
			{
				_db.Open();

				updated = _db.Update(translation.Alpha2, translation.Language, translation.Name);
			}

			return updated;
		}
	}
}