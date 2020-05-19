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
            var inputPath = $"{projectDirectory}/Assets/ptbr.xml";
            var manualTranslationsPath = $"{projectDirectory}/Assets/manual_translations.xml";
            
            StartApp(inputPath, outputPath, manualTranslationsPath).Wait();
        }

        private static async Task StartApp(string inputPath, string outputPath, string manualTranslationsPath)
        {
            var loader = new XmlLoader();
            var document = loader.Load(inputPath);
            var manualTranslations = loader.Load(manualTranslationsPath);
            var translator = new Translator();
            var localizationManager = new DivinityLocalizationTranslator(translator, document);
            var manualTranslationsApplier = new ManualTranslationsApplier(translator, manualTranslations, document);
            
            Console.WriteLine("Translating...");    
            // await localizationManager.TranslateAll(() => loader.Save(outputPath, document));
            
            Console.WriteLine("Applying manual translations...");  
            await manualTranslationsApplier.Apply(() => loader.Save(outputPath, document));
            loader.Save(outputPath, document);
            Console.WriteLine("Finished...");
        }
    }
}