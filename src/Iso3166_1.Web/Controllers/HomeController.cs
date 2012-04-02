using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ServiceStack.ServiceClient.Web;

namespace Iso3166_1.Crowdsource_it.org.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Countries()
		{
			Uri baseUri = Request.Url;
			Uri countriesEn = new Uri(baseUri, "/api/countries/en"),
				countryEn = new Uri(baseUri, "/api/country/DK"),
				countriesFr = new Uri(baseUri, "/api/countries/fr"),
				countryFr = new Uri(baseUri, "/api/country/{0}/fr");

			ViewBag.UrlForCountries = countriesFr.ToString();
			ViewBag.UrlForCountry = countryFr.ToString();

			using (var client = new JsonServiceClient())
			{
				ViewBag.Countries = getCountries(client, countriesEn);

				ViewBag.Country = getRawCountry(client, countryEn);
			}
			return View();
		}

		private string getRawCountry(JsonServiceClient client, Uri countryEn)
		{
			// since contryEn does not include language, it is added to the header
			client.LocalHttpWebRequestFilter = h => h.Headers.Add(HttpRequestHeader.AcceptLanguage, "en");
			var countryResponse = client.Get<string>(countryEn.ToString());
			return countryResponse;
		}

		private IEnumerable<SelectListItem> getCountries(JsonServiceClient client, Uri countriesEn)
		{
			var countriesResponse = client.Get<Api.Messages.CountriesResponse>(countriesEn.ToString());
			return (countriesResponse != null && countriesResponse.ResponseStatus == null) ?
				countriesResponse.Countries.Select(c => new SelectListItem { Value = c.Alpha2_Code, Text = c.Name }) :
				Enumerable.Empty<SelectListItem>();
		}

		public ActionResult Translations()
		{
			ViewBag.GetUrl = new Uri(Request.Url, "/api/translation/{0}/{1}");
			
			return View();
		}

		public ActionResult Create(Models.Translation model)
		{
			using (var client = new JsonServiceClient())
			{
				var postTo = new Uri(Request.Url, "/api/translation");
				var translation = new Api.Messages.Translation
				{
					Code = model.Alpha2,
					Language = model.Language,
					Data = model.Name
				};
				var response = client.Post<Api.Messages.TranslationResponse>(postTo.ToString(), translation);
			}
			return Redirect("Translations");
		}

		public ActionResult Update(Models.Translation model)
		{
			using (var client = new JsonServiceClient())
			{
				Uri putTo = new Uri(Request.Url, string.Format("/api/translation/{0}/{1}", model.Alpha2, model.Language));
				var translation = new Api.Messages.Translation
				{
					Data = model.Name
				};
				var response = client.Put<Api.Messages.TranslationResponse>(putTo.ToString(), translation);
			}
			return Redirect("Translations");
		}
	}
}
