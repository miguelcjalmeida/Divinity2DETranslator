using System.Linq;
using System.Xml;

namespace Divinity2DETranslator
{
    public class GeneralTranslationMistakesFixer
    {
        private readonly XmlDocument _document;

        public GeneralTranslationMistakesFixer(XmlDocument document)
        {
            _document = document;
        }

        public void FixAll()
        {
            var contents = _document.SelectNodes("contentList/content")
                .Cast<XmlNode>()
                .ToList();
            
            foreach (var content in contents)
                FixContent(content);
        }

        private void FixContent(XmlNode content)
        {
            content.InnerText = content.InnerText?.Replace("[Traduzir", "[Translate")
                .Replace("Traduzido por ", "Traduzido por miguelcjalmeida e ");
        }
    }
}