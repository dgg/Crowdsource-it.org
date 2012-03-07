using System;
using Iso3166_1.Crowdsource_it.org.Web.Api;
using Iso3166_1.Crowdsource_it.org.Web.Api.Messages;
using Iso3166_1.Tests.Api.Support;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;

namespace Iso3166_1.Tests.Api
{
	[TestFixture]
	public class CountriesShortcutTester : ShortcutTester
	{
		[Test]
		public void Get_Xml_XmlCodifiedLanguages()
		{
			var repository = Substitute.For<ILanguageRepository>();
			Replacing(repository);
			repository.FindAll().Returns(new[] { "es", "da" });

			object response = Host.ExecuteService(new Languages(), EndpointAttributes.Xml | EndpointAttributes.HttpHead) as LanguagesResponse;

			Assert.That(response, Is.InstanceOf<LanguagesResponse>());
			
			/*using (var client = new XmlServiceClient())
			{
				var response = client.Get<LanguagesResponse>(new Uri(BaseUrl, "/languages").ToString());
				Assert.That(response.ResponseStatus, Is.Null);
				Assert.That(response.Languages[0].Code, Is.EqualTo("es"));
				Assert.That(response.Languages[1].Code, Is.EqualTo("da"));
			}*/
		}
	}
}
