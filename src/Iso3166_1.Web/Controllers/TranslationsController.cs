using System;
using System.Web.Mvc;
using ServiceStack.ServiceClient.Web;

namespace Iso3166_1.Crowdsource_it.org.Web.Controllers
{
    public class TranslationsController : Controller
    {
		public ActionResult Index()
		{
			var urlTemplate = new Uri(Request.Url, "/api/translation/{0}/{1}");
			ViewBag.GetUrl = urlTemplate;

			return View();
		}

		public ActionResult Create(Models.Translation model)
		{
			using (var client = new JsonServiceClient())
			{
				var postUrl = new Uri(Request.Url, "/api/translation");
				var translation = new Api.Messages.Translation
				{
					Code = model.Alpha2,
					Language = model.Language,
					Data = model.Name
				};
				var response = client.Post<Api.Messages.TranslationResponse>(postUrl.ToString(), translation);
			}
			return Redirect("Index");
		}

		public ActionResult Update(Models.Translation model)
		{
			using (var client = new JsonServiceClient())
			{
				var putUrl = new Uri(Request.Url, string.Format("/api/translation/{0}/{1}", model.Alpha2, model.Language));
				var translation = new Api.Messages.Translation
				{
					Data = model.Name
				};
				var response = client.Put<Api.Messages.TranslationResponse>(putUrl.ToString(), translation);
			}
			return Redirect("Index");
		}

		public ActionResult Delete(Models.Translation model)
		{
			using (var client = new JsonServiceClient())
			{
				var deleteUrl = new Uri(Request.Url, string.Format("/api/translation/{0}/{1}", model.Alpha2, model.Language));
				var response = client.Delete<Api.Messages.TranslationResponse>(deleteUrl.ToString());
			}
			return Redirect("Index");
		}
    }
}
