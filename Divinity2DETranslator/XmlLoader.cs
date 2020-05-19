using System;
using System.IO;
using System.Xml;

namespace Divinity2DETranslator
{
    public class XmlLoader
    {
        public XmlDocument Load(string filepath)
        {
            var document = new XmlDocument();
            document.Load(filepath);
            return document;
        }

        public void Save(string filepath, XmlDocument document)
        {
            try 
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filepath));
                document.Save(filepath);
            } 
            catch (Exception)
            {
                Console.WriteLine("Failed at saving document");
            }
        }
    }
}