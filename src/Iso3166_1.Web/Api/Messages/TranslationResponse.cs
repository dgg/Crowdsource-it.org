using System;
using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Messages
{
	public class TranslationResponse : IHasResponseStatus
	{
		public bool Success { get; set; }
		public string Uri { get; set; }
		public ResponseStatus ResponseStatus { get; set; }

		public static IHttpError LanguageNotSupported()
		{
			return new HttpError(new TranslationResponse(), HttpStatusCode.BadRequest, "LanguageNotSupported", "The language needs to be supported");
		}

		public static IHttpError AlreadyExists()
		{
			return new HttpError(new TranslationResponse(), HttpStatusCode.MethodNotAllowed, "EntityExists", "The resource already exists. Use PUT for updates");
		}

		public static TranslationResponse New(IRequestContext request, Translation message)
		{
			var baseUrl = new Uri(request.AbsoluteUri, UriKind.Absolute);

			return new TranslationResponse
			{
				Success = true,
				Uri = Translation.Route.Write(baseUrl, message.Code, message.Language).AbsoluteUri
			};
		}
	}
}