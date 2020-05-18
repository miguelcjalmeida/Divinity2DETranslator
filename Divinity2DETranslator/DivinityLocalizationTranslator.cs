using System;
using System.Xml;

namespace Divinity2DETranslator
{
    public class DivinityLocalizationTranslator
    {
        private readonly Translator _translator;
        private readonly XmlDocument _document;

        public DivinityLocalizationTranslator(Translator translator, XmlDocument document)
        {
            _translator = translator;
            _document = document;
        }

        public void TranslateAll()
        {
            
        }
    }
}