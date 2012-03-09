using System.Globalization;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	internal class CacheHash
	{
		private readonly string _value;
		public CacheHash(CultureInfo ci)
		{
			_value = "Countries." + ci.NeutralName();
		}

		public string Value { get { return _value; } }
	}
}