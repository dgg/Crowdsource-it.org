using System.Globalization;

namespace Iso3166_1.Crowdsource_it.org.Web.Api
{
	public interface ITranslationRepository
	{
		bool Exists(string alpha2_Code, CultureInfo language);
	}
}