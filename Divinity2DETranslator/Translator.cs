namespace Divinity2DETranslator
{
    public class Translator
    {
        public bool IsInEnglish(string text)
        {
            return true;
        }
        
        public string Translate(string englishText)
        {
            /*
            Console.WriteLine("Hello World!");
            TranslationClient client = TranslationClient.Create();
            TranslationResult result = client.TranslateText("It is raining.", LanguageCodes.Portuguese);
            Console.WriteLine($"Result: {result.TranslatedText}; detected language {result.DetectedSourceLanguage}");
            */
            return englishText;
        }
    }
}