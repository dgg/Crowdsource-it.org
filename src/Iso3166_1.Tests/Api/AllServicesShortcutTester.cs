using System.Globalization;
using System.Net;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using Iso3166_1.Crowdsource_it.org.Web.Models;
using Iso3166_1.Tests.Api.Support;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

using CountryMsg = Iso3166_1.Crowdsource_it.org.Web.Api.Messages.Country;
using CountryMdl = Iso3166_1.Crowdsource_it.org.Web.Models.Country;

namespace Iso3166_1.Tests.Api
{
	[TestFixture, Category("Integration")]
	public class AllServicesShortcutTester : ShortcutTester
	{
		#region LanguageService

		[Test]
		public void Languages_Found_OkHttpResult()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] { "es", "da" });

			object response = Host.ExecuteService(new Languages(), EndpointAttributes.Xml | EndpointAttributes.HttpGet);

			Assert.That(response, Is.InstanceOf<IHttpResult>());
			var result = (HttpResult)response;
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result.Response, Is.InstanceOf<LanguagesResponse>());
		}

		[Test]
		public void Languages_NotFoundFound_NotFoundHttpResult()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new string[0]);

			object response = Host.ExecuteService(new Languages(), EndpointAttributes.Json | EndpointAttributes.HttpGet);

			Assert.That(response, Is.InstanceOf<IHttpResult>());
			var result = (IHttpError)response;
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		#endregion

		#region CountryService

		[Test]
		public void Countries_Found_OkHttpResult()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.FindCurrent(Arg.Any<CultureInfo>()).Returns(
				new[]{ new CountryMdl {Translation = new Translation {Name = "Dinamarca"}} });

			object response = Host.ExecuteService(new Countries {Language = "es"},
				EndpointAttributes.Xml | EndpointAttributes.HttpGet);

			Assert.That(response, Is.InstanceOf<IHttpResult>());
			var result = (HttpResult) response;
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result.Response, Is.InstanceOf<CountriesResponse>());
		}

		[Test]
		public void Countries_NotFoundFound_NotFoundHttpResult()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.FindCurrent(Arg.Any<CultureInfo>()).Returns(new CountryMdl[0]);

			object response = Host.ExecuteService(new Countries {Language = "es"},
				EndpointAttributes.Json | EndpointAttributes.HttpGet);


			Assert.That(response, Is.InstanceOf<IHttpResult>());
			var result = (IHttpError) response;
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		[Test]
		public void Country_Found_OkHttpResult()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.Get(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns(
				new CountryMdl { Translation = new Translation {Name = "Dinamarca"} });

			object response = Host.ExecuteService(new CountryMsg {Language = "es", Code = "DK"},
				EndpointAttributes.Xml | EndpointAttributes.HttpGet);

			Assert.That(response, Is.InstanceOf<HttpResult>());
			var result = (HttpResult) response;
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result.Response, Is.InstanceOf<CountryResponse>());
		}

		[Test]
		public void Country_NotFoundFound_NotFoundHttpResult()
		{
			var repository = Substitute.For<ICountryRepository>();
			Replacing(repository);
			repository.Get(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns(default(CountryMdl));

			object response = Host.ExecuteService(new CountryMsg {Language = "es"},
				EndpointAttributes.Json | EndpointAttributes.HttpGet);

			Assert.That(response, Is.InstanceOf<IHttpResult>());
			var result = (IHttpError) response;
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
		}

		#endregion
	}
}
