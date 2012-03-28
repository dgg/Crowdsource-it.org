using System.Globalization;
using System.Net;
using System.Web;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using ServiceStack.Common.Web;
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

			if (Repository.Exists(request.Code, language))
			{
				return Messages.TranslationResponse.AlreadyExists();
			}

			var model = request.ToModel();

			Repository.Create(model);

			return model.ToResponse(()=> Messages.TranslationResponse.New(RequestContext, request));
		}


		// PUT and PATCH are for changing existing entities
		public override object OnPut(Messages.Translation request)
		{
			return base.OnPut(request);
		}

		// PUT and PATCH are for changing existing entities
		public override object OnPatch(Messages.Translation request)
		{
			return base.OnPatch(request);
		}
	}
}