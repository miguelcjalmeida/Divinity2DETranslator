using System;
using System.IO;
using System.Threading.Tasks;

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
            
            StartApp(inputPath, outputPath).Wait();
        }

        private static async Task StartApp(string inputPath, string outputPath)
        {
            var loader = new XmlLoader();
            var document = loader.Load(inputPath);
            var localizationManager = new DivinityLocalizationTranslator(new Translator(), document);
            
            Console.WriteLine("Translating...");    
            await localizationManager.TranslateAll(() => loader.Save(outputPath, document));
            loader.Save(outputPath, document);
            Console.WriteLine("Finished...");
        }

        private static void SaveBatch()
        {
            
        }
    }
}