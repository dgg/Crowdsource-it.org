﻿using System.Web.Mvc;

namespace Iso3166_1.Crowdsource_it.org.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC!";

			return View();
		}

		public ActionResult Demo()
		{
			return View();
		}
	}
}