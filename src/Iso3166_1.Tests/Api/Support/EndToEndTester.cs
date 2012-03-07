using System;
using NUnit.Framework;

namespace Iso3166_1.Tests.Api.Support
{
	public abstract class EndToEndTester
	{
		private TestHost _host;
		protected TestHost Host { get { return _host; } }

		private readonly Uri _baseUrl = new Uri("http://localhost:8880/");
		public Uri BaseUrl { get { return _baseUrl; } }

		[TestFixtureSetUp]
		public void SetUp()
		{
			_host = new TestHost();
			_host.Init();

			_host.Start(_baseUrl.ToString());
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			_host.Stop();
			_host.Dispose();
			_host = null;
		}

		protected EndToEndTester Replacing<T>(T dependency)
		{
			_host.Register(dependency);
			return this;
		}

		protected Uri Urifor(string restRelativeUri)
		{
			return new Uri(BaseUrl, restRelativeUri);
		}

		protected string UrlFor(string restRelativeUri)
		{
			return Urifor(restRelativeUri).ToString();
		}
	}
}
