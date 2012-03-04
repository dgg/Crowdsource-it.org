using System;
using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public static class ResponseExtensions
	{
		public static IHttpResult ToResponse<TModel, TResponse>(this TModel model, Func<TResponse> response) where TModel : class
		{
			return model == null ? (IHttpResult)new HttpError(HttpStatusCode.NotFound, "Not found") : new HttpResult(response(), HttpStatusCode.OK);
		}
	}
}