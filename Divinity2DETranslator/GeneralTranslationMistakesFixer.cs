using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Divinity2DETranslator
{
    public class GeneralTranslationMistakesFixer
    {
        private readonly XmlDocument _document;
        private IList<XmlNode> _contents;

        public GeneralTranslationMistakesFixer(XmlDocument document)
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
                
            UpdateContentText("h004d3fdcgd2f8g4463g8754gcab93b476501", (x) => $"[1] {x}");
            UpdateContentText("h28a17da5g7c91g472cgb750ga7278913d1a3", (x) => $"[{x}");
            UpdateContentText("h28fdef15g034eg459bgbb93g1e22004b738e", (x) => $"[{x}");
            UpdateContentText("h42f0b1b6gd786g46ebga04egf56b57d92048", (x) => $"{x} Aumenta dano em [1]%");
            UpdateContentText("h5060d9ecgcfeag41ccg8ac8gfd2e56d6a79b", (x) => $"{x} Cada um causando [1]");
            UpdateContentText("h53f1f4d5gf664g4cb3gac8dgb4fbd2651d51", (x) => x.Replace("[2]", "2"));
            UpdateContentText("h5ad3165dgd2fcg4522g8814g9e02f0d691a7", (x) => $"{x} - [1]");
            UpdateContentText("h5c80b7f5g7b79g40bag87f7gb811f9755421", (x) => $"[{x}");
            UpdateContentText("hc1b9af2agfcb0g442aga1a2gd5aa8d597508", (x) => $"[{x}");
            UpdateContentText("h7aeeb71fg8db4g4e39g967bg4cd2a9e50e4c", (x) => x.Replace("[1]", "dano físico"));
            UpdateContentText("h85e53977gcfa7g4946g8a24gdea79fd5b508", (x) => x.Replace("[1 segundo", "[1] segundo"));
            UpdateContentText("h8b7f2ab0g091bg4fdcgb337g212dac7751dd", (x) => $"Destrói [1]. {x}");
            UpdateContentText("h8b82ac18g7c5bg4163g9afdg6ea934063c38", (x) => $"[FORA-DA-LEI] {x}");
            UpdateContentText("h8bdf3310g8b01g47e8ga55dgc0da8b6dd0cc", (x) => $"[FORA-DA-LEI] {x}");
            UpdateContentText("h8dea788agabbag4f9ag8fa0gdcf5557e554e", (x) => $"{x} Recebe [1]% de dano adicional por alvo, contando totems. ");
            UpdateContentText("h8fb876c6g25ecg45a3gbd7cg6faf59082783", (x) => x.Replace("[IE_Interact]", "Mover"));
            UpdateContentText("h9729b9ceg2860g4396g8b3fg9eef86106d4f", (x) => $"{x} Aumenta dano em [2]%");
            UpdateContentText("h99856a3egef40g4b3agb531ga3c254635463", (x) => $"{x} Reduz resistências em [1]%");
            UpdateContentText("h9cd84cafg7edfg4f92g9ef1g07d329e56900", (x) => x.Replace("[Você", "Você").Replace("atacar.]", "atacar."));
            UpdateContentText("haac8a2efgf2c8g43c3g98eag6adab34b8c5a", (x) => "Condense todas as superfícies de nuvens na área. Causa [1] a inimigos e apaga fogo.");
            UpdateContentText("hb1f9cd59g0a38g4dfdga781g4acbb5e02aa3", (x) => "Uma granada tão incrível que pode assustar os inimigos em um raio de [1].");
            UpdateContentText("hb6ed6709g7b22g4b96g942fg9b7de50f4335", (x) => "Esta granada desencadeia uma reação química que causa [1] em um raio de [2], podendo congelar tudo ao alcance.");
            UpdateContentText("hb6ef5200g4cd4g48dag8006gac3dc3e8aa51", (x) => "Enquanto se esconde, [1] aumenta o dano de ataque em [2]%. Também reduz o custo de entrar no modo furtivo em [3] PA.");
            UpdateContentText("hc5444aadg242eg4b64gbad8g2664de231b44", (x) => "A Granada de Mão Sagrada curará significativamente todos em um raio de [1].");
            UpdateContentText("he519b121g3a65g4422g8e5dgd9f65b0cce7c", (x) => "Amaldiçoe o alvo para explodir após um turno ou após a morte, causando [1] em 3m de raio.");
            UpdateContentText("hf1cbbfacg4f75g4e8cg93c1ga7072fc4aa07", (x) => x.Replace("[RightStickY]", "[IE_CameraZoomIn]"));
            UpdateContentText("hfb851683g738cg4b48g8fd8g478d375c030a", (x) => "Encarnado aprende uma habilidade de teletransporte e uma habilidade para trocar de lugar com seu mestre. Aumenta o dano em [1]%.");
            UpdateContentText("h25fae2cfgb16eg452fg991bg1185acdc5cda", (x) => x.Replace("[IE_Pause]", "[IE_ToggleInGameMenu]"));
            UpdateContentText("h372b47d2gc0b9g4838g9ae7g9350cf91923c", (x) => "[FORA-DA-LEI]");
        }

        private void FixContent(XmlNode content)
        {
            content.InnerText = content.InnerText?
                .Replace("[Traduzir", "[Translate")
                .Replace("Traduzido por Agência Luma", "Traduzido por Agência Luma e miguelcjalmeida")
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