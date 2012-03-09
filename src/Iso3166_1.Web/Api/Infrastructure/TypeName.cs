using System;
using System.CodeDom;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class TypeName
	{
		public static string Friendly(Type type)
		{
			using (var provider = new Microsoft.CSharp.CSharpCodeProvider())
			{
				var @ref = new CodeTypeReference(type);
				return provider.GetTypeOutput(@ref);
			}
		}

		public static string Friendly(object o)
		{
			return (o == null) ? "NULL" : Friendly(o.GetType());
		}
	}
}