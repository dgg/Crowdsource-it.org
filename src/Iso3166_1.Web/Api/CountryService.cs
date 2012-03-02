using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class CountryService : IService<Countries>, IService<Country>
	{
		public object Execute(Countries request)
		{
			return new CountriesResponse
			{
				Countries = new[]
				{
					new Resources.Country{ Alpha2_Code = "DK", Alpha3_Code = "DKK", Numeric_Code = 208, Name = "Denmark" },
					new Resources.Country{ Alpha2_Code = "ES", Alpha3_Code = "ESP", Numeric_Code = 724, Name = "Spain" }
				}
			};
		}

		public object Execute(Country request)
		{
			return new Resources.Country {Alpha2_Code = request.Code, Alpha3_Code = "DKK", Numeric_Code = 208, Name = "Denmark"};
		}
	}
}