using System;
using System.IO;
using ServiceStack.ServiceHost;
using YamlDotNet.RepresentationModel.Serialization;

namespace Iso3166_1.Crowdsource_it.org.Web.Api.Infrastructure
{
	public class ApplicationFormat
	{
		// servie stack won't cope with properly defined content types
		//public static readonly string ContentType = "application/prs.Crowdsource-it.org.Iso3166-1+yaml";
		public static readonly string ContentType = "application/prs-iso3166-1-yaml";

		public static void ToStream(IRequestContext request, object response, Stream stream)
		{
			using (var sw = new StreamWriter(stream))
			{
				var serializer = new YamlSerializer();
				serializer.Serialize(sw, response);
			}
		}

		public static object FromStream(Type type, Stream stream)
		{
			throw new NotImplementedException("Application format is output-only");
		}
	}
}