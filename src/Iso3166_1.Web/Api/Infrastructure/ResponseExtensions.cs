using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public static class ResponseExtensions
	{
		public static IHttpResult ToResponse<TModel, TResponse>(this TModel model, Func<TResponse> response) where TModel : class
		{
			//throw new Exception("an empty collection is also a 404. create an overload");
			return model == null ? (IHttpResult)new HttpError(HttpStatusCode.NotFound, "Not found") : new HttpResult(response(), HttpStatusCode.OK);
		}

		public static IHttpResult ToResponse<TModel, TResponse>(this IEnumerable<TModel> model, Func<TResponse> response) where TModel : class
		{
			//throw new Exception("an empty collection is also a 404. create an overload");
			return !(model ?? Enumerable.Empty<TModel>()).Any() ? (IHttpResult)new HttpError(HttpStatusCode.NotFound, "Not found") : new HttpResult(response(), HttpStatusCode.OK);
		}
	}
}