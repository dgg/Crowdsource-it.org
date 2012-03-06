using Cassette.Configuration;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Iso3166_1.Crowdsource_it.org.Web
{
	/// <summary>
	/// Configures the Cassette asset modules for the web application.
	/// </summary>
	public class CassetteConfiguration : ICassetteConfiguration
	{
		public void Configure(BundleCollection bundles, CassetteSettings settings)
		{
			bundles.AddPerIndividualFile<ScriptBundle>("scripts/pages");
			bundles.AddPerSubDirectory<ScriptBundle>("scripts",
				new ExcludeDirectorySearch("*.js", new[] { "pages" }));

			bundles.Add<StylesheetBundle>("content");
		}
	}
}