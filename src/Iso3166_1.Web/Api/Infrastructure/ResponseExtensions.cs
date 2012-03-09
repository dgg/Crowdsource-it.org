using System;
using System.Collections.Generic;
using System.Data;
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
			return model == null ? notFound<TModel>() : found(response);
		}

		public static IHttpResult ToResponse<TModel, TResponse>(this IEnumerable<TModel> model, Func<TResponse> response) where TModel : class
		{
			return !(model ?? Enumerable.Empty<TModel>()).Any() ? notFound<IEnumerable<TModel>>() : found(response);
		}

		private static IHttpResult found<TResponse>(Func<TResponse> response)
		{
			return new HttpResult(response(), HttpStatusCode.OK);
		}

		private static IHttpResult notFound<TModel>()
		{
			return new HttpError(HttpStatusCode.NotFound, TypeName.Friendly(typeof(TModel)));
		}
	}
}