using System;
using System.Net;
using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class PrepareCountryQueryAttribute : Attribute, IHasRequestFilter
	{
		public void RequestFilter(IHttpRequest req, IHttpResponse res, object requestDto)
		{
			var msg = requestDto as Country;
			if (msg == null ||
				string.IsNullOrEmpty(msg.Code) ||
				msg.Code.Length != 2)
			{
				res.StatusCode = (int)HttpStatusCode.BadRequest;
				res.End();
			}
		}
	}
}