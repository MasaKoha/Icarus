using Icarus.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Icarus.Editor.Test
{
    public class IcarusLocalizerTest
    {
        [Test]
        public void ConvertTextToDic()
        {
            TextLocalizer.Initialize("ja", "ja", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "テスト");

            TextLocalizer.Initialize("en", "en", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "Test");

            TextLocalizer.Initialize("en", "en", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "Test");
        }

        [Test]
        public void KeyNotFoundTest()
        {
            TextLocalizer.Initialize("ja", "ja", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            try
            {
                TextLocalizer.GetText("NotExistKey");
            }
            catch (KeyNotFoundException)
            {
                // KeyNotFoundException が出れば成功
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void LastLineBrTest()
        {
            TextLocalizer.Initialize("en", "en", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "Test");

            TextLocalizer.Initialize("en", "en", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "Test");
        }

        [Test]
        public void GetDefaultTextTest()
        {
            TextLocalizer.Initialize("ja", "en", $"key,ja,en{Environment.NewLine}KeyTest,テスト{Environment.NewLine}KeyTest2,テスト2,Test2{Environment.NewLine}KeyTest3,テスト3,Test3");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "テスト");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest2"), "Test2");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest3"), "Test3");
        }

        [Test]
        public void TargetLanguageStringEmptyTest()
        {
            TextLocalizer.Initialize("ja", "en", $"key,ja,en{Environment.NewLine}KeyTest,テスト{Environment.NewLine}KeyTest2,テスト2,{Environment.NewLine}KeyTest3,テスト3,Test3");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "テスト");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest2"), "テスト2");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest3"), "Test3");
        }

        [Test]
        public void FailConvertTextToDic()
        {
            // Duplicated Key
            try
            {
                TextLocalizer.Initialize("ja", "ja", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            }
            catch (ArgumentException e)
            {
                Debug.Log($"Test OK :{e.Message}");
                return;
            }
            Assert.Fail();
        }

        [Test]
        public void LocalizationEnumKey()
        {
            TextLocalizer.Initialize("ja", "ja", $"key,ja,en{Environment.NewLine}KeyTest,テスト,Test{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest"), "テスト");
        }

        [Test]
        public void GetTextWithArgsTest()
        {
            TextLocalizer.Initialize("ja", "ja", $"key,ja{Environment.NewLine}KeyTest,テスト{{0}}{{1}}{{2}}{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest", "引数", "の", "Test"), "テスト引数のTest");
            TextLocalizer.Initialize("ja", "ja", $"key,ja{Environment.NewLine}KeyTest,テスト{{0}}{{1}}{{2}}{Environment.NewLine}");
            Assert.AreEqual(TextLocalizer.GetText("KeyTest", "引数", "の", "Test", "ですよ"), "テスト引数のTest");
            TextLocalizer.Initialize("ja", "ja", $"key,ja{Environment.NewLine}KeyTest,テスト{{0}}{{1}}{{2}}{Environment.NewLine}");
            try
            {
                TextLocalizer.GetText("KeyTest", "引数", "の");
            }
            catch
            {
                // catch していれば成功
                return;
            }

            Assert.Fail();
        }
    }
}