using System.Globalization;
using System.Net;
using EasyHttp.Http;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using Iso3166_1.Tests.Api.Support;
using NSubstitute;
using NUnit.Framework;

namespace Iso3166_1.Tests.Api
{
	[TestFixture, Category("Integration")]
	public class CountriesEndToEndTester : EndToEndTester
	{
		[Test]
		public void Countries_Empty_NotFound()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.FindCurrent(Arg.Any<CultureInfo>()).Returns(new Country[] { });

			var client = new HttpClient();
			client.Request.AcceptLanguage = "en";
			HttpResponse response = client.Get(UrlFor("/countries"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		[Test]
		public void Country_Get_NotEmptyBody()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			string translation = "España";
			repository.Get("ES", Arg.Any<CultureInfo>()).Returns(
				new Country { Translation = new Translation { Name = translation}});

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationJson;
			HttpResponse response = client.Get(UrlFor("/country/ES/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(response.ContentType, Is.EqualTo(HttpContentTypes.ApplicationJson));
			Assert.That(response.DynamicBody.name, Is.EqualTo(translation));
		}

		[Test]
		public void Country_HeadOverExisting_Ok()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.Get("ES", Arg.Any<CultureInfo>()).Returns(
				new Country { Translation = new Translation() });

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationXml;
			HttpResponse response = client.Head(UrlFor("/country/ES/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(response.ContentType, Is.EqualTo(HttpContentTypes.ApplicationXml));
			Assert.That(response.RawText, Is.Empty);
		}

		[Test]
		public void Country_HeadOverMissing_NotFound()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.Get("ES", Arg.Any<CultureInfo>()).Returns(default(Country));

			var client = new HttpClient();
			client.Request.Accept = HttpContentTypes.ApplicationXml;
			HttpResponse response = client.Head(UrlFor("/country/ES/en"));

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
			Assert.That(response.ContentType, Is.Null);
		}
	}
}
