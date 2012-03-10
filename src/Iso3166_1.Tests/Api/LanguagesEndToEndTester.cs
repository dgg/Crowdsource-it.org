using System.Data;
using System.Net;
using EasyHttp.Http;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure;
using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using Iso3166_1.Tests.Api.Support;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.ServiceClient.Web;

namespace Iso3166_1.Tests.Api
{
	[TestFixture, Category("Integration")]
	public class LanguagesEndToEndTester : EndToEndTester
	{
		[Test]
		public void Get_Xml_XmlCodifiedLanguages()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] {"es", "da"});

			using (var client = new XmlServiceClient())
			{
				var response = client.Get<LanguagesResponse>(UrlFor("/languages"));
				Assert.That(response.ResponseStatus, Is.Null);
				Assert.That(response.Languages[0].Code, Is.EqualTo("es"));
				Assert.That(response.Languages[1].Code, Is.EqualTo("da"));
			}
		}

		[Test]
		public void Get_Json_JsonCodifiedLanguages()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] { "de", "en" });

			using (var client = new JsonServiceClient())
			{
				var response = client.Get<LanguagesResponse>(UrlFor("/languages"));
				Assert.That(response.ResponseStatus, Is.Null);
				Assert.That(response.Languages[0].Code, Is.EqualTo("de"));
				Assert.That(response.Languages[1].Code, Is.EqualTo("en"));
			}
		}

		[Test]
		public void MethodNotSupported_NotFoundException()
		{
			using (var client = new JsonServiceClient())
			{
				var ex = Assert.Throws<WebServiceException>(()=> client.Post<string>(
						UrlFor("/languages"), 
						null));

				Assert.That(ex.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
			}
		}

		[Test]
		public void FormatNotSupported_HtmlReturned()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] { "es" });

			var client = new HttpClient
			{
				Request = { Accept = HttpContentTypes.ApplicationOctetStream }
			};

			HttpResponse response = client.Get(UrlFor("/languages"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(response.RawHeaders[HttpResponseHeader.ContentType], Is.EqualTo(HttpContentTypes.TextHtml));
		}

		[Test]
		public void Format_CanBeOverridenWithQueryString()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new []{"es"});

			var client = new HttpClient
			{
				Request = { Accept = HttpContentTypes.ApplicationJson }
			};

			HttpResponse response = client.Get(UrlFor("/languages?format=xml"));

			Assert.That(response.ContentType, Is.EqualTo(HttpContentTypes.ApplicationXml));
		}

		[Test]
		public void Logging_ATestingLoggerCanBeUsed_ForStateTesting()
		{
			Replacing(Substitute.For<ILanguageRepository>());
			Replacing<ILogFactory>(new TestLogFactory());
			
			var client = new HttpClient
			{
				Request = { Accept = HttpContentTypes.ApplicationJson }
			};

			client.Get(UrlFor("/languages"));

			var logs = TestLogger.GetLogs();
			Assert.That(logs[0].Key, Is.EqualTo(TestLogger.Levels.WARN));
			Assert.That(logs[0].Value, Is.StringContaining("IEnumerable<string>"));
		}

		[Test]
		public void Logging_DoublesCanBeUsed_ForInteractionTesting()
		{
			var factory = Substitute.For<ILogFactory>();
			var log = Substitute.For<ILog>();
			factory.GetLogger(Arg.Any<string>()).Returns(log);
			Replacing(factory);
			Replacing(Substitute.For<ILanguageRepository>());

			var client = new HttpClient
			{
				Request = { Accept = HttpContentTypes.ApplicationJson }
			};

			client.Get(UrlFor("/languages"));

			log.Received().Warn(Arg.Any<string>(), Arg.Any<ObjectNotFoundException>());
		}

		[Test]
		public void Get_ApplicationFormat_YamlResponse()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] { "es", "da" });

			var client = new HttpClient
			{
				Request = { Accept = ApplicationFormat.ContentType }
			};

			var response = client.Get(UrlFor("/languages"));

			Assert.That(response.RawText, Is.EqualTo(
@"Languages:
- Code: es
- Code: da
"));
		}

		[Test]
		public void Get_ApplicationFormatOverride_YamlResponse()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] { "es", "da" });

			var client = new HttpClient();

			var response = client.Get(UrlFor("/languages?format=prs-iso3166-1-yaml"));

			Assert.That(response.RawText, Is.EqualTo(
@"Languages:
- Code: es
- Code: da
"));

		}
	}
}
