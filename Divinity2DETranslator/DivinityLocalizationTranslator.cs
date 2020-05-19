using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Divinity2DETranslator
{
    public class DivinityLocalizationTranslator
    {
        private readonly int _parallelTranslations = 100;
        private readonly Translator _translator;
        private readonly XmlDocument _document;

        public DivinityLocalizationTranslator(Translator translator, XmlDocument document)
        {
            _translator = translator;
            _document = document;
        }

        public async Task TranslateAll(Action batchFinished)
        {
            var contents = _document.SelectNodes("contentList/content")
                .Cast<XmlNode>()
                .ToList();
            
            var logger = new DivinityLocalizationLogger(contents.Count);
            var batches = contents.SplitIntoListsOfGivenSize(_parallelTranslations);
            await TranslateBatchesParallelly(batches, logger, batchFinished);
        }

        private async Task TranslateBatchesParallelly(IEnumerable<IList<XmlNode>> batches,
            DivinityLocalizationLogger logger, Action batchFinished)
        {
            foreach (var batchNode in batches) {
                var tasks = batchNode.Select(x => TranslateContent(x, logger));
                await Task.WhenAll(tasks);
                batchFinished();
            }
        }

        private async Task TranslateContent(XmlNode content, DivinityLocalizationLogger logger)
        {
            await TranslateContentIfNeeded(content);
            logger.LogTranslationDone();
        }

        private async Task TranslateContentIfNeeded(XmlNode content)
        {
            var translatingText = content.InnerText;
            var isInEnglish = await _translator.IsInEnglish(translatingText);
            if (!isInEnglish) return;

            var translation = await _translator.Translate(translatingText);
            content.InnerText = translation;
        }
    }
}