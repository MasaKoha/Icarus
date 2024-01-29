using System;
using Icarus.Core;
using NUnit.Framework;
using UnityEngine;

namespace Icarus.Editor.Test
{
    public class CsvConverterTest
    {
        [Test]
        public void CommaTextTest()
        {
            var oneLineTestCsv = Resources.Load<TextAsset>("CommaAndDoubleQuotesTest").text;

            var texts = CsvParser.Parse(oneLineTestCsv);

            for (var i = 0; i < texts.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        Assert.AreEqual("a,c",texts[i]);
                        break;
                    case 1:
                        Assert.AreEqual("\"a\"",texts[i]);
                        break;
                    case 2:
                        Assert.AreEqual("\"a,c\"",texts[i]);
                        break;
                    case 3:
                        Assert.AreEqual("a\"b,c\"",texts[i]);
                        break;
                    case 4:
                        Assert.AreEqual("\"b,c\"a",texts[i]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}