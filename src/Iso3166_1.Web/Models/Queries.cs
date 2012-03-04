using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Dapper;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	public static class Queries
	{
		public static Country Country(this IDbConnection cn, string alpha2Code, CultureInfo language)
		{
			return cn.Query<Country, Translation, Country>(
				"SELECT * FROM Countries AS c INNER JOIN Translations AS t ON c.Alpha2 = t.Alpha2 WHERE c.Alpha2 = @code AND t.Language = @lang",
				(c, t) => c.Translated(t),
				new { code = alpha2Code, lang = language.Name },
				splitOn: "Alpha2")
				.SingleOrDefault();
		}

		public static IEnumerable<Country> AllCountries(this IDbConnection cn, CultureInfo language)
		{
			return cn.Query<Country, Translation, Country>(
				"SELECT * FROM Countries AS c INNER JOIN Translations AS t ON c.Alpha2 = t.Alpha2 WHERE t.Language = @lang",
				(c, t) => c.Translated(t),
				new { lang = language.Name },
				splitOn: "Alpha2");
		}
	}
}