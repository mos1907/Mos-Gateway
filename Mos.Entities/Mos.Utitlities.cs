using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Messaging;

namespace Mos.Utilities
{
    public static class Extensions
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false); // no BOM in a .NET string
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(writer, toSerialize, ns);
                    return stringWriter.ToString();
                }
            }
        }

        public static byte[] SerializeObjectInByteArray<T>(this T toSerialize)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false); // no BOM in a .NET string
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
                {
                    xmlSerializer.Serialize(writer, toSerialize, ns);
                    return Encoding.ASCII.GetBytes(stringWriter.ToString());
                }
            }
        }
        public static T DeserializeFromString<T>(this string toDesrialize) where T : class
        {
            try
            {
                using (TextReader reader = new StringReader(toDesrialize))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    return (T)ser.Deserialize(reader);
                    //return ser.Deserialize(reader) as T;
                }
            }
            catch (Exception ex)
            {
                return default(T); // is this really the right approach?  Just ignore the error and silently return null?
            }


           
        }
    }
}
