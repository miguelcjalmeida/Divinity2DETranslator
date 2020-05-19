using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Divinity2DETranslator
{
    public class ManualTranslationsApplier
    {
        private readonly Translator _translator;
        private readonly XmlDocument _manualTranslations;
        private readonly XmlDocument _iaTranslations;
        private readonly int _parallelTranslations = 100;
        
        public ManualTranslationsApplier(Translator translator, XmlDocument manualTranslations, XmlDocument iaTranslations)
        {
            _translator = translator;
            _manualTranslations = manualTranslations;
            _iaTranslations = iaTranslations;
        }
        
        public async Task Apply(Action batchFinished) 
        {
            var contents = _manualTranslations.SelectNodes("contentList/content")
                            .Cast<XmlNode>()
                            .ToList();
                            
            var logger = new DivinityLocalizationLogger(contents.Count);
            var batches = contents.SplitIntoListsOfGivenSize(_parallelTranslations);
            await ApplyThePortugueseOnes(batches, logger, batchFinished);
        }
        
        private async Task ApplyThePortugueseOnes(IEnumerable<IList<XmlNode>> batches,
            DivinityLocalizationLogger logger, Action batchFinished)
        {
            foreach (var batchNode in batches) {
                var tasks = batchNode.Select(x => ApplyWhenContentIsPortugueseLogging(x, logger));
                await Task.WhenAll(tasks);
                batchFinished();
            }
        }

        private async Task ApplyWhenContentIsPortugueseLogging(XmlNode content, DivinityLocalizationLogger logger)
        {
            await ApplyWhenContentIsPortuguese(content);
            logger.LogTranslationDone();
        }

        private async Task ApplyWhenContentIsPortuguese(XmlNode content)
        {
            var contentText = content.InnerText;
            var isInPortuguese = await _translator.IsInPortuguese(contentText);
            if (!isInPortuguese) return;

            var id = content.Attributes["contentuid"]?.Value;
            SetIaTranslationNodeText(id, contentText);
        }

        private void SetIaTranslationNodeText(string id, string text)
        {
            if (id == null) return;
            var iaTranslationNode = _iaTranslations.SelectSingleNode($"contentList/content[@contentuid='{id}']");
            if (iaTranslationNode == null) return;
            iaTranslationNode.InnerText = text;
        }
    }
}