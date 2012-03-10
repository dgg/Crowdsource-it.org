using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public class CountryService : IService<Messages.Countries>, IService<Messages.Country>, IRequiresRequestContext
	{
		public IRequestContext RequestContext { get; set; }
		public ICountryRepository Repository { get; set; }

		public object Execute(Messages.Countries request)
		{
			CultureInfo language = RequestContext.Language(request.Language);

			return RequestContext.EndpointAttributes.HasFlag(EndpointAttributes.HttpHead) ?
				areCountriesTranslatedInto(language) :
				findCountriesIn(language);
		}

		private object areCountriesTranslatedInto(CultureInfo language)
		{
			bool exist = Repository.AreTranslated(language);
			return exist.ToResponse<Messages.CountriesResponse>();
		}

		public object findCountriesIn(CultureInfo language)
		{
			IEnumerable<Country> model = Repository.FindCurrent(language);

			return model.ToResponse(() => new Messages.CountriesResponse
			{
				Countries = model.Select(toResource).ToArray()
			});
		}

		public Resources.Country toResource(Country model)
		{
			return new Resources.Country
			{
				Alpha2_Code = model.Alpha2,
				Alpha3_Code = model.Alpha3,
				Numeric_Code = model.Numeric,
				Name = model.Translation.Name
			};
		}

		public object Execute(Messages.Country request)
		{
			CultureInfo language = RequestContext.Language(request.Language);

			return RequestContext.EndpointAttributes.HasFlag(EndpointAttributes.HttpHead) ?
				doesCountryExistIn(request.Code, language) :
				getCountryIn(request.Code, language);
		}

		private object doesCountryExistIn(string alpha2Code, CultureInfo language)
		{
			bool exist = Repository.Exists(alpha2Code, language);
			return exist.ToResponse<Messages.CountriesResponse>();
		}

		public object getCountryIn(string alpha2Code, CultureInfo language)
		{
			Country model = Repository.Get(alpha2Code, language);

			return model.ToResponse(() => new Messages.CountryResponse
			{
				Country = toResource(model)
			});
		}
	}
}