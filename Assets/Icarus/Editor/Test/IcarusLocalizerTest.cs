using Icarus.Core;
using NUnit.Framework;

namespace Icarus.Editor.Test
{
    public class IcarusLocalizerTest
    {
        public void LoadFile()
        {
            var file = FileLoader.Load();
            Assert.AreNotEqual(file, null);
        }

        public void ConvertTextToDic()
        {
            TextLocalizer.Initialize("ja", "key,ja,en\nKeyTest,テスト,Test\n");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "テスト");

            TextLocalizer.Initialize("en", "key,ja,en\nKeyTest,テスト,Test\n");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "Test");
        }
    }
}