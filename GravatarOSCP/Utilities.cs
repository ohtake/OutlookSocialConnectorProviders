using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Fujitsu.GravatarOSCP
{
	static class Utilities
	{
		public static string SerializeObjectToString(object o)
		{
			using (StringWriter writer = new StringWriter())
			{
				Type t = o.GetType();
				XmlSerializer xs = new XmlSerializer(t);
				xs.Serialize(writer, o);
				return writer.ToString();
			}
		}

		public static T DeserializeStringToObject<T>(string xml) where T : class
		{
			using (StringReader reader = new StringReader(xml))
			{
				XmlSerializer xs = new XmlSerializer(typeof(T));
				return xs.Deserialize(reader) as T;
			}
		}
	}
}
