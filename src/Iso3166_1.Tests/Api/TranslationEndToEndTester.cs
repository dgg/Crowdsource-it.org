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
		public void GET_NotAvailableLanguage_NotFound()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);

			var client = new HttpClient();
			HttpResponse response = client.Get(UrlFor("/translation/DK/es-ES"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

			repository.DidNotReceive().Get(Arg.Any<string>(), Arg.Any<CultureInfo>());
		}

		[Test]
		public void GET_DidNotExist_NotFound()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Get(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns(default(TranslationMdl));

			var client = new HttpClient();
			HttpResponse response = client.Get(UrlFor("/translation/DK/es"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		[Test]
		public void GET_Existing_OK()
		{
			string code = "DK", language = "es", translation = "Dinamarca";
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Get(code, Arg.Is(CultureInfo.GetCultureInfo(language))).Returns(
				new TranslationMdl{Alpha2 = code, Language = language, Name = translation});

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			HttpResponse response = client.Get(UrlFor("/translation/DK/es"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(response.DynamicBody.success, Is.True);
			Assert.That(response.DynamicBody.translation.data, Is.EqualTo(translation));

		}

		[Test]
		public void HEAD_NotSupported()
		{
			var client = new HttpClient();
			HttpResponse response = client.Head(UrlFor("/translation/DK/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
		}

		[Test]
		public void POST_LanguageNotSupported_BadRequest()
		{
			var client = new HttpClient();
			HttpResponse response = client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = "ES", Language = "notSupported", Data = "something" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public void POST_AlreadyExists_MethodNotAllowed()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Create(Arg.Any<TranslationMdl>()).Returns(false);

			var client = new HttpClient();
			HttpResponse response = client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = "DK", Language = "es", Data = "Dinamarca" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
		}

		[Test]
		public void POST_DidNotExist_ResourceCreated()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Create(Arg.Any<TranslationMdl>()).Returns(true);

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			HttpResponse response = client.Post(UrlFor("/translation"),
				new TranslationMsg { Code = "DK", Language = "es", Data = "Dinamarca" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

			Assert.That(response.DynamicBody.success, Is.True);
			var expectedUri = new Uri(BaseUrl, new Uri("/translation/DK/es", UriKind.Relative));
			Assert.That(response.DynamicBody.uri, Is.EqualTo(expectedUri.ToString()));

		}

		[Test]
		public void POST_DidNotExist_TranslationCreated()
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

		[Test]
		public void PUT_WrongUrl_NotFound()
		{
			var client = new HttpClient();
			HttpResponse response = client.Put(UrlFor("/translation"),
				new TranslationMsg(),
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		[Test]
		public void PUT_LanguageNotSupported_BadRequest()
		{
			var client = new HttpClient();
			HttpResponse response = client.Put(UrlFor("/translation/ES/notSupported"),
				new TranslationMsg { Data = "something" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public void PUT_AlreadyExists_Ok()
		{
			string code = "DK", language = "es", data = "Dinamarca";

			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Update(
				Arg.Is<TranslationMdl>(m =>
					m.Language == language &&
					m.Alpha2 == code &&
					m.Name == data))
				.Returns(true);

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			HttpResponse response = client.Put(UrlFor("/translation/{0}/{1}", code, language),
				new TranslationMsg { Data = data },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			Assert.That(response.DynamicBody.success, Is.True);
			var expectedUri = new Uri(BaseUrl, new Uri("/translation/DK/es", UriKind.Relative));
			Assert.That(response.DynamicBody.uri, Is.EqualTo(expectedUri.ToString()));
		}

		[Test]
		public void PUT_DidNotExist_NotFound()
		{
			var repository = Substitute.For<ITranslationRepository>();
			Replacing(repository);
			repository.Update(Arg.Any<TranslationMdl>()).Returns(false);

			var client = new HttpClient();
			HttpResponse response = client.Put(UrlFor("/translation/DK/es"),
				new TranslationMsg { Data = "Dinamarca" },
				HttpContentTypes.ApplicationJson);

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}
	}
}
