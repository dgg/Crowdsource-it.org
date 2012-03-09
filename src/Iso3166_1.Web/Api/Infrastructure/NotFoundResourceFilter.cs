using System;
using System.Data;
using System.Net;
using ServiceStack.Logging;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class NotFoundResourceFilter : IHasResponseFilter
	{
		public void ResponseFilter(IHttpRequest req, IHttpResponse res, object responseDto)
		{
			// all services should return an IHttpResult (tested convention)
			var result = responseDto as IHttpResult;
			if (result == null) throwUnconventionalResult(responseDto);
			else
			{
				if (result.StatusCode == HttpStatusCode.NotFound)
				{
					var log = req.TryResolve<ILogFactory>();

					var ex = new ObjectNotFoundException(((IHttpError) result).ErrorCode);
					log.GetLogger(string.Empty).Warn("No results returned", ex);
				}
			}
		}

		private void throwUnconventionalResult(object responseDto)
		{
			string message = string.Format("The result of a service call must be of type '{0}' but was '{1}'",
				TypeName.Friendly(typeof (IHttpResult)),
				TypeName.Friendly(responseDto));
			throw new ArgumentException(message, "responseDto");
		}
	}
}