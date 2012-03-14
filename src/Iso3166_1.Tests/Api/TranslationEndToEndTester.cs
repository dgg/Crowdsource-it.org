using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using EasyHttp.Http;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Tests.Api.Support;
using NSubstitute;
using NUnit.Framework;

namespace Iso3166_1.Tests.Api
{
	[TestFixture, Category("Integration")]
	public class TranslationEndToEndTester : EndToEndTester
	{
		[Test]
		public void GET_Translation_NotAvailableLanguage_NotFound()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			//repository.Exists(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns(new Country[] { });

			var client = new HttpClient();
			HttpResponse response = client.Get(UrlFor("/translation/DK/es-ES"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

			repository.DidNotReceive().Exists(Arg.Any<string>(), Arg.Any<CultureInfo>());
		}

		[Test]
		public void GET_Translation_DoesNotExist_NotFound()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Exists(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns(false);

			var client = new HttpClient();
			HttpResponse response = client.Get(UrlFor("/translation/DK/es"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		[Test]
		public void GET_Translation_Existing_OK()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Exists("DK", Arg.Is(CultureInfo.GetCultureInfo("en"))).Returns(true);

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			HttpResponse response = client.Get(UrlFor("/translation/DK/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void HEAD_Translation_NotSupported()
		{
			//var repository = Substitute.For<ITranslationRepository>();
			//Replacing(repository);
			//repository.Exists(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns(new Country[] { });

			var client = new HttpClient();
			HttpResponse response = client.Head(UrlFor("/translation/DK/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
		}

		[Test]
		public void Post_Translation_EXPECTATION()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Exists("DK", Arg.Is(CultureInfo.GetCultureInfo("en"))).Returns(true);

			var client = new HttpClient();
			HttpResponse response = client.Post(UrlFor("/translation"), new{A = "a"}, HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			
		}
	}
}
