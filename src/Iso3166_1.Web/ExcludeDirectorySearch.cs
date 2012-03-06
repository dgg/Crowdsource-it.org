using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Cassette.Configuration;

namespace Iso3166_1.Crowdsource_it.org.Web
{
	/// <summary>
	/// An exclude directory search for Cassette. Provide the patterns you want to search for
	/// and this will exclude *.min/*-vsdoc files as well as the directories you specify.
	/// </summary>
	public class ExcludeDirectorySearch : FileSearch
	{
		/// <summary>
		/// Excludes specified directories in search (also .min and -vsdoc files)
		/// </summary>
		/// <param name="pattern">File search pattern (wildcards, e.g. *.css;*.less)</param>
		/// <param name="directoriesToExclude">A string array of folder names to exclude. Will match anywhere in full file path.</param>
		public ExcludeDirectorySearch(string pattern, string[] directoriesToExclude)
		{
			SearchOption = SearchOption.AllDirectories;
			Pattern = pattern;
			ExcludeDirectories = directoriesToExclude;
		}

		private string[] ExcludeDirectories
		{
			set
			{
				// Join with rx | (or)
				var directories = String.Join("|", value);

				// Extensions "*.js;*.coffee" => "js", "coffee"
				// Assumes you're using wildcard... but whatever.
				var extensions = String.Join("|", Pattern.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries).
				                                  	Select(s => s.Substring(2)).ToArray());

				var excludeRegex =
					new Regex(String.Format(@"(
		    {0}                  # Directory exclusions
		)[\\/]|([\.-](
		    vsdoc       |        # Vsdoc files                    
		    min                  # Minified files
		)\.({1})$)", directories, extensions), RegexOptions.IgnorePatternWhitespace);

				// Set exclusions
				Exclude = excludeRegex;
			}
		}
	}
}