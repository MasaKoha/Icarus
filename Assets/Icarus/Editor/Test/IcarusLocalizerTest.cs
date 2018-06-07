using Icarus.Core;
using NUnit.Framework;

namespace Icarus.Editor.Test
{
    public class IcarusLocalizerTest
    {
        [Test]
        public void LoadFile()
        {
            var file = FileLoader.Load();
            Assert.AreNotEqual(file, null);
        }

        [Test]
        public void ConvertTextToDic()
        {
            TextLocalizer.Initialize("ja", "key,ja,en\nKeyTest,テスト,Test\n");
            Assert.AreEqual(TextLocalizer.LocalizedText["KeyTest"], "テスト");

            TextLocalizer.Initialize("en", "key,ja,en\nKeyTest,テスト,Test\n");
            Assert.AreEqual(TextLocalizer.LocalizedText["KeyTest"], "Test");
        }
    }
}