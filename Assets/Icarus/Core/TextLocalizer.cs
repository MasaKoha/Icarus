using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Icarus.Core
{
    public static class TextLocalizer
    {
        private static ReadOnlyDictionary<string, string> _localizedText;

        public static string GetText(string key) => _localizedText[key];

        /// <summary>
        /// Initialize
        /// If TargetRawText is string.Empty, Load Text in Resource/LocalizeText
        /// </summary>
        public static void Initialize(string language = "ja", string targetRawText = "")
        {
            string rawText = "";

            rawText = targetRawText == "" ? FileLoader.Load().text : targetRawText;

            _localizedText = GetLocalizedText(language, rawText);
        }

        private static ReadOnlyDictionary<string, string> GetLocalizedText(string language, string text)
        {
            var dic = new Dictionary<string, string>();
            var keyValueLine = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            var langAttribute = keyValueLine[0].Split(',');
            int languageIndex = 0;

            for (int i = 0; i < langAttribute.Length; i++)
            {
                if (language == langAttribute[i])
                {
                    languageIndex = i;
                }
            }

            if (languageIndex == 0)
            {
                throw new Exception($"Language \"{language}\" not Exists");
            }

            for (int rawTextLine = 1; rawTextLine < keyValueLine.Length - 1; rawTextLine++)
            {
                // "//" : Comment out
                if (keyValueLine[rawTextLine].Contains("//"))
                {
                    continue;
                }

                var keyValue = keyValueLine[rawTextLine].Split(',');

                // keyValue.Length が 1 以下だと、正しく設定されていないもしくは空白行部分なので避ける
                if (keyValue.Length <= 1)
                {
                    continue;
                }

                dic.Add(keyValue[0], keyValue[languageIndex]);
            }

            return new ReadOnlyDictionary<string, string>(dic);
        }
    }
}