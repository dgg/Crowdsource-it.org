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

		public Translation Get(string alpha2_Code, CultureInfo language)
		{
			using (_db)
			{
				_db.Open();
				Translation model = _db.Translation(alpha2_Code, language.NeutralName());
				return model;
			}
		}

		public bool Create(Translation toBeCreated)
		{
			using (_db)
			{
				_db.Open();
				bool exists = _db.CurrentExists("Staged_Translations", toBeCreated.Alpha2, toBeCreated.Language);
				if (exists)
				{
					return false;
				}
				_db.Insert(toBeCreated.Alpha2, toBeCreated.Language, toBeCreated.Name);
				return true;
			}
		}

		public bool Update(Translation toBeUpdated)
		{
			bool updated;
			using (_db)
			{
				_db.Open();

				updated = _db.Update(toBeUpdated.Alpha2, toBeUpdated.Language, toBeUpdated.Name);
			}

			return updated;
		}

		public bool Delete(Translation toBeDeleted)
		{
			bool deleted;
			using (_db)
			{
				_db.Open();

				deleted = _db.Delete(toBeDeleted.Alpha2, toBeDeleted.Language);
			}

			return deleted;
		}
	}
}