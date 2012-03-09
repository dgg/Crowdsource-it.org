using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public sealed class Available
	{
		private readonly Dictionary<string, CultureInfo> _inner;
		private Available()
		{
			_inner = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
				.Where(ci => !ci.Equals(CultureInfo.InvariantCulture))
				.ToDictionary(ci => ci.Name, ci => ci, StringComparer.OrdinalIgnoreCase);
		}

		private static  readonly Lazy<Available> _lazy = new Lazy<Available>(()=> new Available());

		public static Available Languages { get { return _lazy.Value; } }

		public bool ContainsKey(string key)
		{
			return _inner.ContainsKey(key);
		}

		public bool ContainsValue(CultureInfo value)
		{
			return _inner.ContainsValue(value);
		}

		public bool TryGetValue(string key, out CultureInfo value)
		{
			return _inner.TryGetValue(key, out value);
		}

		public CultureInfo GetOrInvariant(string key)
		{
			CultureInfo ci;
			return TryGetValue(key, out ci) ? ci : CultureInfo.InvariantCulture;
		}

		public IEqualityComparer<string> Comparer
		{
			get { return _inner.Comparer; }
		}

		public int Count
		{
			get { return _inner.Count; }
		}

		public CultureInfo this[string key]
		{
			get { return _inner[key]; }
		}

		public ICollection<CultureInfo> Values
		{
			get { return _inner.Values; }
		}

		public ICollection<string> Keys
		{
			get { return _inner.Keys; }
		}
	}
}