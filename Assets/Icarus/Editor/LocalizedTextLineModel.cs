using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Icarus.Editor
{
    public class LocalizedTextLineModel
    {
        public readonly int LineNo;

        public readonly LocalizedTextLineType Type;

        public readonly string LineText;

        public readonly string Key;

        public readonly string[] LocalizedTexts;

        public LocalizedTextLineModel(int lineNo, string lineBody)
        {
            LineNo = lineNo;
            LineText = lineBody;
            Type = GetLocalizeTextLineType(LineText);
            Key = "";
            LocalizedTexts = new string[] { "" };

            if (Type == LocalizedTextLineType.KeyValue)
            {
                string[] keyValue = LineText.Split(',');
                Key = keyValue[0];
                LocalizedTexts = keyValue.Skip(1).ToArray();
            }
        }

        private LocalizedTextLineType GetLocalizeTextLineType(string line)
        {
            if (line.StartsWith("//"))
            {
                return LocalizedTextLineType.Comment;
            }

            if (line.StartsWith("key,"))
            {
                return LocalizedTextLineType.Header;
            }

            string[] keyValue = line.Split(',');

            // ',' で区切られていないこれまでに無い Type は Other に属する
            if (keyValue.Length == 1)
            {
                return LocalizedTextLineType.Other;
            }

            return LocalizedTextLineType.KeyValue;
        }
    }
}