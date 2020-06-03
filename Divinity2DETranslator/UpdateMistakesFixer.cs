using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Divinity2DETranslator
{
    public class UpdateMistakesFixer
    {
        private readonly XmlDocument _document;
        private IList<XmlNode> _contents;

        public UpdateMistakesFixer(XmlDocument document)
        {
            _document = document;
        }

        public void FixAll()
        {
            _contents = _document.SelectNodes("contentList/content")
                .Cast<XmlNode>()
                .ToList();
            
            foreach (var content in _contents)
                FixContent(content);
            
            UpdateContentText("hd0e1a2c8g8327g493dg9ce9g2526cfc99848", (x) => "Um flash surpresa que pode infligir cegos em [1] raio. Cria uma pequena superfície de lodo com um raio [2].");
        }

        private void FixContent(XmlNode content)
        {
            content.InnerText = content.InnerText?
                .Replace("Um flash surpresa que pode infligir cegos em um raio.", "Um flash surpresa que pode infligir cegos em [1] raio.")
                .Replace("[PLATAFORMA]", "[FORA-DA-LEI]")
                .Replace("[MISTÍCO]", "[MÍSTICO]")
                .Replace("[JURAMENTO]", "[JURADO]")
                .Replace("[JURAR]", "[JURADO]")
                .Replace("[BotãoEsquerdo]", "[LeftStick]")
                .Replace("[ANO]", "[ANÃO]")
                .Replace("[FORA]", "[FORA-DA-LEI]")
                .Replace("[DESLOCADO]", "[FALECIDO]")
                .Replace("[DESLIGADO]", "[FALECIDO]")
                .Replace("[ FALECIDO]", "[FALECIDO]")
                .Replace("[JUROS]", "[JURADO]")
                .Replace("[FORLAÇÃO]", "[FORA-DA-LEI]")
                .Replace("[PROIBIDO]", "[OUTLAW]")
                .Replace("[ 7]", "[7]")
                .Replace("[FORAGIDO]", "[FORA-DA-LEI]")
                .Replace("[BANDIDO]", "[FORA-DA-LEI]")
                .Replace("[ESCOLAR]", "[INTELECTUAL]")
                .Replace("[FORATA]", "[FORA-DA-LEI]")
                .Replace("[FORA DE FORA]", "[FORA-DA-LEI]")
                .Replace("[Iniciar]", "[Start]")
                .Replace("[FORA DA LEI]", "[FORA-DA-LEI]");
        }

        private void UpdateContentText(string identifier, Func<string, string> getTextUpdated)
        {
            var content = _contents
                .FirstOrDefault(x => x.Attributes["contentuid"].Value == identifier);
            
            if (content == null) return;
            
            content.InnerText = getTextUpdated(content.InnerText);
        }  
    }
}