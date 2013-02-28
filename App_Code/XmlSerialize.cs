using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

/// <summary>
/// This is used to store objects inside of the database, mainly the report templates
/// </summary>
/// Andrew Heim 1/22/2013
public static class XmlSerialize
{

    public static string Serialize<T>(this Object obj)
    {
        StringBuilder str = new StringBuilder();
        using (XmlWriter xml = XmlWriter.Create(str))
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            x.Serialize(xml, obj);
        }

        return str.ToString();
    }

    public static T Desrialize<T>(string str)
    {
        using (XmlReader xml = XmlReader.Create(new StringReader(str)))
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
            return (T)x.Deserialize(xml);
        }
    }
}