using System;
using System.IO;

namespace Divinity2DETranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent?.Parent?.FullName;
            var outputPath = $"{projectDirectory}/Output/english.xml";
            var inputPath = $"{projectDirectory}/Assets/english.xml";
            
            var loader = new XmlLoader();
            var document = loader.Load(inputPath);
            
            var localizationManager = new DivinityLocalizationTranslator(new Translator(), document);
            localizationManager.TranslateAll();
            loader.Save(outputPath, document);
        }
    }
}