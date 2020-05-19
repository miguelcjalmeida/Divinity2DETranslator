using System;
using System.Threading;
using System.Threading.Tasks;
using Google;
using Google.Cloud.Translation.V2;

namespace Divinity2DETranslator
{
    public class Translator
    {
        private readonly TranslationClient _client = TranslationClient.Create();
        private readonly string _targetLanguage = LanguageCodes.Portuguese;
        private readonly string _sourceLanguage = LanguageCodes.English;
        
        public async Task<bool> IsInEnglish(string text)
        {
            return await TryFewTimes(async () =>
            {
                var detection = await _client.DetectLanguageAsync(text);
                return detection.Language == _sourceLanguage;
            });
        }
        
        public async Task<bool> IsInPortuguese(string text)
        {
            return await TryFewTimes(async () =>
            {
                var detection = await _client.DetectLanguageAsync(text);
                return detection.Language == _targetLanguage;
            });
        }

        public async Task<string> Translate(string englishText)
        {
            var translation = await TryFewTimes(async () =>
            {
                var results = await _client.TranslateTextAsync(englishText, _targetLanguage, _sourceLanguage);
                return results.TranslatedText;
            });
            
            return translation ?? englishText;
        }

        private async Task<T> TryFewTimes<T>(Func<Task<T>> doAction, int tryCount=1, int maxCount=100)
        {
            try
            {
                if (tryCount >= maxCount) return default;
                return await doAction();
            } 
            catch (GoogleApiException ex)
            {
                if (ex.Error == null) return default;
                var delay = 1000 * (10 + tryCount);
                Console.WriteLine($"error but trying again soon ({delay}ms): {ex.Error.Code} - {ex.Error.Message}");
                Thread.Sleep(delay);       
                return await TryFewTimes(doAction, tryCount + 1, maxCount);
            }
            catch (Exception) 
            {
                Console.WriteLine($"unknown error, making few more tries {tryCount}/{maxCount}");
                Thread.Sleep(1000);    
                return await TryFewTimes(doAction, tryCount + 1, maxCount);
            }
        }
    }
}