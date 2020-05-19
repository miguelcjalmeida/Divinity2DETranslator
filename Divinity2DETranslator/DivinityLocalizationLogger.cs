using System;

namespace Divinity2DETranslator
{
    public class DivinityLocalizationLogger
    {
        private readonly int _totalTranslations;
        private int _done = 0;
        private int _doneRate = 0;
        
        public DivinityLocalizationLogger(int totalTranslations)
        {
            _totalTranslations = totalTranslations;
        }

        public void LogTranslationDone()
        {
            _doneRate = (100 * ++_done / _totalTranslations);
            PrintLog();
        }

        private void PrintLog() => Console.WriteLine($"status {_done}/{_totalTranslations} ({_doneRate}%)");
    }
}