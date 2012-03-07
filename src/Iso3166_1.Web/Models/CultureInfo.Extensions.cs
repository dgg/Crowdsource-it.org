using System;
using System.Globalization;

namespace Iso3166_1.Crowdsource_it.org.Web.Models
{
	public static class CultureInfoExtensions
	{
		public static string NeutralName(this CultureInfo ci)
		{
			if (ci == null) throw new ArgumentNullException("ci");

			return ci.IsNeutralCulture ? ci.Name : ci.Parent.Name;
		}
	}
}