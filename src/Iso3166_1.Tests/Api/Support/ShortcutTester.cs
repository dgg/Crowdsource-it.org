using Iso3166_1.Crowdsource_it.org.Web.App_Start;
using NUnit.Framework;

namespace Iso3166_1.Tests.Api.Support
{
	public abstract class ShortcutTester
	{
		private AppHost _host;
		protected AppHost Host { get { return _host; } }

		[TestFixtureSetUp]
		public void SetUp()
		{
			_host = new AppHost();
			_host.Init();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			_host.Dispose();
			_host = null;
		}

		protected ShortcutTester Replacing<T>(T dependency)
		{
			_host.Register(dependency);
			return this;
		}
	}
}