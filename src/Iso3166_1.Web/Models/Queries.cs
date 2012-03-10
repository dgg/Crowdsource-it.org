using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	public static class Queries
	{
		public static IEnumerable<Country> CurrentCountries(this IDbConnection cn, string language)
		{
			return cn.Query<Country, Translation, Country>(@"
SELECT *
FROM Countries AS c INNER JOIN Translations AS t
	ON c.Alpha2 = t.Alpha2
WHERE
	t.Language = @lang AND
	c.Obsolete = 0
ORDER BY t.Name",
				(c, t) => c.Translated(t),
				new { lang = language },
				splitOn: "Alpha2");
		}

		public static bool CurrentExist(this IDbConnection cn, string language)
		{
			return cn.Query<bool>(@"
SELECT 
    (CASE 
        WHEN EXISTS(
            SELECT NULL AS [EMPTY]
            FROM Countries AS c INNER JOIN Translations AS t
				ON c.Alpha2 = t.Alpha2
			WHERE
				t.Language = @lang AND
				c.Obsolete = 0
            ) THEN 1
        ELSE 0
     END) AS [exists]
",
			new { lang = language }).Single();
		}

		public static Country Country(this IDbConnection cn, string alpha2Code, string language)
		{
			return cn.Query<Country, Translation, Country>(@"
SELECT *
FROM Countries AS c INNER JOIN Translations AS t
	ON c.Alpha2 = t.Alpha2
WHERE
	c.Alpha2 = @code AND
	t.Language = @lang",
				(c, t) => c.Translated(t),
				new { code = alpha2Code, lang = language },
				splitOn: "Alpha2")
				.SingleOrDefault();
		}

		public static bool CurrentExists(this IDbConnection cn, string alpha2Code, string language)
		{
			return cn.Query<bool>(@"
SELECT 
    (CASE 
        WHEN EXISTS(
            SELECT NULL AS [EMPTY]
            FROM Countries AS c INNER JOIN Translations AS t
				ON c.Alpha2 = t.Alpha2
			WHERE
				c.Alpha2 = @code AND
				t.Language = @lang
            ) THEN 1
        ELSE 0
     END) AS [exists]
",
			new { code = alpha2Code, lang = language }).Single();
		}

		public static IEnumerable<Country> AllCountries(this IDbConnection cn, string language)
		{
			return cn.Query<Country, Translation, Country>(@"
SELECT *
FROM Countries AS c INNER JOIN Translations AS t
ON c.Alpha2 = t.Alpha2
WHERE
	t.Language = @lang
ORDER BY t.Name",
				(c, t) => c.Translated(t),
				new { lang = language },
				splitOn: "Alpha2");
		}

		public static IEnumerable<string> AvailableLanguages(this IDbConnection cn)
		{
			return cn.Query<string>("SELECT DISTINCT(Language) FROM Translations");
		}
	}
}