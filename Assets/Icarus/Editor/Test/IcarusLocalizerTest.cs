using Icarus.Core;
using NUnit.Framework;
using System;

namespace Icarus.Editor.Test
{
    public class IcarusLocalizerTest
    {
        public void LoadFile()
        {
            var file = FileLoader.Load();
            Assert.AreNotEqual(file, null);
        }

        [Test]
        public void ConvertTextToDic()
        {
            TextLocalizer.Initialize("ja", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "テスト");

            TextLocalizer.Initialize("en", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "Test");
        }

        [Test]
        public void LocalizationEnumKey()
        {
            TextLocalizer.Initialize("ja", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText(LocalizationEnum.KeyTest), "テスト");
        }

        [Test]
        public void GetTextWithArgsTest()
        {
            TextLocalizer.Initialize("ja", $"key,ja{Environment.NewLine}KeyTest,テスト{{0}}{{1}}{{2}}{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest", "引数", "の", "Test"), "テスト引数のTest");
        }
    }
}