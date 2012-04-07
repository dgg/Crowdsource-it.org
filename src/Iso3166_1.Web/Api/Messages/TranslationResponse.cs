using System;
using System.Net;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	public class TranslationResponse : IHasResponseStatus
	{
		public bool Success { get; set; }
		public string Uri { get; set; }
		public Translation Translation { get; set; }
		public ResponseStatus ResponseStatus { get; set; }

		public static IHttpError LanguageNotSupported()
		{
			return new HttpError(new TranslationResponse(), HttpStatusCode.BadRequest, "LanguageNotSupported", "The language needs to be supported");
		}

		public static IHttpResult Found(Translation message)
		{
			return new HttpResult(new TranslationResponse
			{
				Success = true,
				Translation = message
			},
			HttpStatusCode.OK);
		}

		public static IHttpError AlreadyExists()
		{
			return new HttpError(new TranslationResponse(), HttpStatusCode.MethodNotAllowed, "EntityExists", "The resource already exists. Use PUT for updates");
		}

		public static IHttpResult Created(IRequestContext request, Translation message)
		{
			return new HttpResult(response(request, message), HttpStatusCode.Created);
		}

		public static IHttpResult Updated(IRequestContext request, Translation message)
		{
			return new HttpResult(response(request, message), HttpStatusCode.OK);
		}

		private static TranslationResponse response(IRequestContext request, Translation message)
		{
			var baseUrl = new Uri(request.AbsoluteUri, UriKind.Absolute);

			var response = new TranslationResponse
			{
				Success = true,
				Uri = Translation.Route.Write(baseUrl, message.Code, message.Language).AbsoluteUri
			};

			return response;
		}

		public static IHttpResult NotFound()
		{
			return new HttpError(HttpStatusCode.NotFound, TypeName.Friendly(typeof(Models.Translation)));
		}

		public static IHttpResult Deleted()
		{
			var response = new TranslationResponse
			{
				Success = true,
			};

			return new HttpResult(response, HttpStatusCode.OK);
		}
	}
}