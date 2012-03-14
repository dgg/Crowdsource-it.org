using System;
using System.Collections.Generic;
using System.Globalization;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class Reserved
	{
		private readonly Dictionary<string, CultureInfo> _inner;
		private Reserved()
		{
			_inner = new Dictionary<string, CultureInfo>(StringComparer.OrdinalIgnoreCase)
			{
				{"en", CultureInfo.GetCultureInfo("en")},
				{"fr", CultureInfo.GetCultureInfo("fr")}
			};
		}

		private static  readonly Lazy<Reserved> _lazy = new Lazy<Reserved>(()=> new Reserved());

		public static Reserved Languages { get { return _lazy.Value; } }

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