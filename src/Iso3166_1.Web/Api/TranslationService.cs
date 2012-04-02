using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using ServiceStack.ServiceInterface;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class TranslationService : RestServiceBase<Messages.Translation>
	{
		public ITranslationRepository Repository { get; set; }

		// unfortunately there is no OnHead

		public override object OnGet(Messages.Translation request)
		{
			CultureInfo language;
			bool exists = false;
			if (Available.Languages.TryGetValue(request.Language, out language))
			{
				exists = Repository.Exists(request.Code, language);
			}
			return exists.ToResponse<Messages.TranslationResponse>();
		}

		// POST is for new entities
		public override object OnPost(Messages.Translation request)
		{
			CultureInfo language;
			if (!Available.Languages.TryGetValue(request.Language, out language))
			{
				return Messages.TranslationResponse.LanguageNotSupported();
			}

			var model = request.ToModel();

			bool created = Repository.Create(model);
			return created ?
				Messages.TranslationResponse.Created(RequestContext, request) :
				Messages.TranslationResponse.AlreadyExists();
		}
		
		// PUT is for changing existing entities
		public override object OnPut(Messages.Translation request)
		{
			CultureInfo language;
			if (!Available.Languages.TryGetValue(request.Language, out language))
			{
				return Messages.TranslationResponse.LanguageNotSupported();
			}
			var model = request.ToModel();
			bool updated = Repository.Update(model);

			return updated ?
				Messages.TranslationResponse.Updated(RequestContext, request) :
				Messages.TranslationResponse.NotFound();
		}
	}
}