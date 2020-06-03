using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using NUnit.Framework;

namespace Divinity2DETranslator.Tests.Acceptance
{
    public class TranslationTagUntouchedTests
    {
        private IList<XmlNode> _origin;
        private IList<XmlNode> _translation;
        private readonly IList<(string, string)> _exceptions = GetTagTranslationExceptions();
        
        [SetUp]
        public void Setup()
        {
            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent?.Parent?.FullName;
            var originPath = $"{projectDirectory}/../Divinity2DETranslator/Assets/english.xml";
            var translationPath = $"{projectDirectory}/../Divinity2DETranslator/Output/update_fixed.xml";
            var xmlLoader = new XmlLoader();

            _origin = xmlLoader.Load(originPath)
                .SelectNodes("contentList/content")
                .Cast<XmlNode>()
                .OrderBy(x => x.Attributes["contentuid"].Value)
                .ToList();
            
            var allTranslation = xmlLoader.Load(translationPath)
                .SelectNodes("contentList/content")
                .Cast<XmlNode>()
                .OrderBy(x => x.Attributes["contentuid"].Value)
                .ToList();

            _translation = allTranslation
                .Intersect(_origin, new TranslationNodeComparer())
                .OrderBy(x => x.Attributes["contentuid"].Value)
                .ToList();
        }

        [Test]
        public void Length_should_be_the_same()
        {
            Assert.AreEqual(_origin.Count, _translation.Count);
        }
        
        [Test]
        public void Tags_should_be_untouched_after_translation()
        {
            for (var i = 0; i < _origin.Count; i++)
            {
                AssertTagsAreUnchanged(
                    _origin[i].InnerText, 
                    _translation[i].InnerText, 
                    _origin[i].Attributes["contentuid"].Value);
            }
        }

        private void AssertTagsAreUnchanged(string original, string translation, string identifier)
        {
            var regex = new Regex("\\[[^\\]]+\\]", RegexOptions.IgnoreCase);
            
            var originalTags = regex.Matches(original)
                .Select(m => m.Value)
                .ToList();
            
            var translationTags = regex.Matches(translation)
                .Select(m => m.Value)
                .ToList();            
            
            Assert.AreEqual(originalTags.Count, translationTags.Count, $"{identifier} tag count doesn't match");

            for (var i = 0; i < originalTags.Count; i++)
                AssertTagIsUnchangedOrAllowedToChange(identifier, originalTags[i], translationTags[i]);
        }

        private void AssertTagIsUnchangedOrAllowedToChange(
            string identifier, string originalTag, string translationTag)
        {
            if (originalTag.Equals(translationTag)) return;
            if (originalTag.Any(x => x == ' ')) return;
            
            var isChangeAllowed = _exceptions.Any(x => 
                x.Item1.ToLower().Equals(originalTag.ToLower()) && 
                x.Item2.ToLower().Equals(translationTag.ToLower()));

            if (isChangeAllowed) return;
            
            Assert.AreEqual(originalTag, translationTag, $"{identifier} tags don't match");
        }

        private static IList<(string, string)> GetTagTranslationExceptions() => new List<(string, string)>
        {
            ("[SCHOLAR]", "[INTELECTUAL]"),
            ("[DWARF]", "[ANÃO]"),
            ("[VILLAIN]", "[VILÃO]"),
            ("[DWARF-FRIEND]", "[AMIGO-ANÃO]"),
            ("[BARBARIAN]", "[BÁRBARO]"),
            ("[NOBLE]", "[NOBRE]"),
            ("[UNDEAD]", "[MORTO-VIVO]"),
            ("[MYSTIC]", "[MÍSTICO]"),
            ("[HERO]", "[HERÓI]"),
            ("[LIZARD]", "[LAGARTO]"),
            ("[ELF]", "[ELFO]"),
            ("[SWORN]", "[JURADO]"),
            ("[BEAST]", "[BESTA]"),
            ("[SOLDIER]", "[SOLDADO]"),
            ("[JESTER]", "[Bobo da Corte]"),
            ("[HUMAN]", "[HUMANO]"),
            ("[JESTER]", "[Brincalhão]"),
            ("[JESTER]", "[BOBO DA CORTE]"),
            ("[BARBARIAN]", "[Bárbaro]"),
            ("[JESTER]", "[BRINCALHÃO]"),
            ("[FANE]", "[Fane]"),
            ("[ELF-FRIEND]", "[AMIGO DOS ELFOS]"),
            ("[UNDEAD]", "[Morto-Vivo]"),
            ("[NOBLE]", "[]"),
            ("[2]", "[1]"),
            ("[1]", "[2]"),
            ("[MYSTIC]", "[Místico]"),
            ("[ELF-FRIEND]", "[ELF-AMIGO]"),
            ("[DWARF-FRIEND]", "[AMIGO ANÃO]"),
            ("[UNDEAD]", "[Morto-vivo]"),
            ("[OUTLAW]", "[FORA-DA-LEI]"),
            ("[CHAMPION]", "[CAMPEÃO]"),
            ("[OUTLAW]", "[FORA DE FORA]"),
            ("[DWARF-FRIEND]", "[AMIGO DO ANO]"),
            ("[DECEASED]", "[FALECIDO]"),
            ("[BARBARIAN]", "[BARBARO]"),
            ("[SWORN]", "[JURADA]"),
            ("[MYSTIC]", "[MISTICO]"),
            ("[MYSTIC]", "[MÍSTICA]"),
        };
    }
}