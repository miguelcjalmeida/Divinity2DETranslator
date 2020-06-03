using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Divinity2DETranslator.Apps
{
    static class UpdateTranslationApp
    {
        public static void Run()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent?.Parent?.FullName;
            var outputPath = $"{projectDirectory}/Output/update.xml";
            var englishPath = $"{projectDirectory}/Assets/english.xml";
            var translatedPath = $"{projectDirectory}/Assets/Translated/english.xml";
            
            StartApp(englishPath, translatedPath, outputPath).Wait();
        }

        private static async Task StartApp(string englishPath, string translatedPath, string outputPath)
        {
            Console.WriteLine("Initializing update...");    
            var loader = new XmlLoader();
            var englishDoc = loader.Load(englishPath);
            var translatedDoc = loader.Load(translatedPath);
            var needingTranslationDoc = CreateNeedingTranslationOnlyDoc(englishDoc, translatedDoc);
            var translator = new Translator();
            var localizationManager = new DivinityLocalizationTranslator(translator, needingTranslationDoc);
            var fixer = new UpdateMistakesFixer(needingTranslationDoc);
            
            Console.WriteLine("Translating new lines...");    
            await localizationManager.TranslateAll(() => {});
            
            Console.WriteLine("Fixing general translation mistakes");
            fixer.FixAll();
            
            Console.WriteLine("Building final artifact...");
            var finalArtifact = CreateDocumentWithUnionOfTwo(needingTranslationDoc, translatedDoc);
            loader.Save(outputPath, finalArtifact);
            Console.WriteLine("Finished...");
        }

        private static XmlDocument CreateDocumentWithUnionOfTwo(XmlDocument doc1, XmlDocument doc2)
        {
            var newDoc = new XmlDocument();
            var contentList = newDoc.CreateElement("contentList");
            newDoc.AppendChild(contentList);

            var doc1Nodes = doc1.SelectNodes("contentList/content").Cast<XmlNode>().ToList();
            var doc2Nodes = doc2.SelectNodes("contentList/content").Cast<XmlNode>().ToList();

            var allNodes = doc1Nodes
                .Union(doc2Nodes)
                .OrderBy(x => x.Attributes["contentuid"].Value)
                .ToList();

            foreach (var node in allNodes)
            {
                var newNode = newDoc.CreateElement("content");
                newNode.SetAttribute("contentuid", node.Attributes["contentuid"].Value);
                newNode.InnerText = node.InnerText;
                contentList.AppendChild(newNode);
            }
            
            return newDoc;
        }

        private static XmlDocument CreateNeedingTranslationOnlyDoc(XmlDocument englishDoc, XmlDocument translatedDoc)
        {
            var doc = new XmlDocument();
            var contentList = doc.CreateElement("contentList");
            doc.AppendChild(contentList);

            var translatedNodes = translatedDoc.SelectNodes($"contentList/content")
                .Cast<XmlNode>();

            var englishNodes = englishDoc.SelectNodes($"contentList/content")
                .Cast<XmlNode>();

            var untranslatedNodes = englishNodes.Except(translatedNodes, new TranslationNodeComparer());
            
            foreach (var untranslatedNode in untranslatedNodes)
            {
                var newNode = doc.CreateElement("content");
                newNode.SetAttribute("contentuid", untranslatedNode.Attributes["contentuid"].Value);
                newNode.InnerText = untranslatedNode.InnerText;
                contentList.AppendChild(newNode);
            }

            return doc;
        }
    }
}