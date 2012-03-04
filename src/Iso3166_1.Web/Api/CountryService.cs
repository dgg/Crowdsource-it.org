using System.Data;
using System.Globalization;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class CountryService : IService<Messages.Countries>, IService<Messages.Country>
	{
		public IDbConnection Db { get; set; }

		public object Execute(Messages.Countries request)
		{
			return new Messages.CountriesResponse
			{
				Countries = new[]
				{
					new Resources.Country{ Alpha2_Code = "DK", Alpha3_Code = "DKK", Numeric_Code = 208, Name = "Denmark" },
					new Resources.Country{ Alpha2_Code = "ES", Alpha3_Code = "ESP", Numeric_Code = 724, Name = "Spain" }
				}
			};
		}

		public object Execute(Messages.Country request)
		{
			using (Db)
			{
				Db.Open();
				Country model = Db.Country(request.Code, CultureInfo.GetCultureInfo("en"));
				
				return model.ToResponse(() => new Resources.Country
				{
					Alpha2_Code = model.Alpha2,
					Alpha3_Code = model.Alpha3,
					Numeric_Code = model.Numeric,
					Name = model.Translation.Name
				});
			}
		}
	}
}