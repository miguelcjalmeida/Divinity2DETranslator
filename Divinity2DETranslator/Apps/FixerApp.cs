using System;
using System.IO;

namespace Divinity2DETranslator.Apps
{
    static class FixerApp
    {
        public static void Run()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent?.Parent?.FullName;
            var inputPath = $"{projectDirectory}/Output/update.xml";
            var outputPath = $"{projectDirectory}/Output/update_fixed.xml";
            
            StartApp(inputPath, outputPath);
        }

        private static void StartApp(string inputPath, string outputPath)
        {
            var loader = new XmlLoader();
            var document = loader.Load(inputPath);
            var fixer = new UpdateMistakesFixer(document);
            
            Console.WriteLine("Fixing update translation mistakes");
            fixer.FixAll();
            
            loader.Save(outputPath, document);
            Console.WriteLine("Finished...");
        }
    }
}