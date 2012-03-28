using System;
using System.Globalization;
using System.Net;
using EasyHttp.Http;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Tests.Api.Support;
using NSubstitute;
using NUnit.Framework;

using TranslationMsg = Iso3166_1.Crowdsource_it.org.Web.Api.Messages.Translation;
using TranslationMdl = Iso3166_1.Crowdsource_it.org.Web.Models.Translation;

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
			var client = new HttpClient();
			HttpResponse response = client.Head(UrlFor("/translation/DK/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
		}

		[Test]
		public void Post_Translation_LanguageNotSupported_Exception()
		{
			var client = new HttpClient();
			HttpResponse response = client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = "ES", Language = "notSupported", Data = "something"},
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
			
		}

		[Test]
		public void Post_Translation_AlreadyExisted_Exception()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Exists("DK", Arg.Is(CultureInfo.GetCultureInfo("es"))).Returns(true);

			var client = new HttpClient();
			HttpResponse response = client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = "DK", Language = "es", Data = "Dinamarca" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
		}

		[Test]
		public void Post_Translation_DidNotExist_OkWithUrl()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			HttpResponse response = client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = "DK", Language = "es", Data = "Dinamarca" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			Assert.That(response.DynamicBody.success, Is.True);
			var expectedUri = new Uri(BaseUrl, new Uri("/translation/DK/es", UriKind.Relative));
			Assert.That(response.DynamicBody.uri, Is.EqualTo(expectedUri.ToString()));
			
		}

		[Test]
		public void Post_Translation_DidNotExist_TranslationCreated()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);

			string code = "DK", language = "es", data = "Dinamarca";
			var client = new HttpClient();
			client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = code, Language = language, Data = data },
				HttpContentTypes.ApplicationJson);

			repository.Received().Create(Arg.Is<TranslationMdl>(m =>
				m.Language == language &&
				m.Alpha2 == code &&
				m.Name == data));
		}
	}
}
