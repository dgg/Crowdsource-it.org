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
			Country model = Repository.Get(request.Code, language);
			return model.ToResponse(() => toResource(model));
		}
	}
}