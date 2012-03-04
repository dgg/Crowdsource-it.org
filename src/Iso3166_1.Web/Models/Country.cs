namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	public class Country
	{
		public string Alpha2 { get; set; }
		public string Alpha3 { get; set; }
		public short Numeric { get; set; }
		public bool Obsolete { get; set; }

		public Translation Translation { get; set; }

		public Country Translated(Translation translation)
		{
			Translation = translation;
			return this;
		}

	}
}