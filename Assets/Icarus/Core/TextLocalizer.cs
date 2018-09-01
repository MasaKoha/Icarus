using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Icarus.Core
{
    public static class TextLocalizer
    {
        private static ReadOnlyDictionary<string, string> _localizedText;

        public static string GetText(LocalizationEnum enumKey)
        {
            var key = enumKey.ToString();
            return GetText(key);
        }

        public static string GetText(string key)
        {
            string text;
            if (!_localizedText.TryGetValue(key, out text))
            {
                throw new KeyNotFoundException("not found key: " + key);
            }
            return text;
        }

        public static string GetText(LocalizationEnum enumKey, params object[] args)
        {
            var key = enumKey.ToString();
            return GetText(key, args);
        }

        public static string GetText(string key, params object[] args)
        {
            string value = string.Empty;
            try
            {
                value = string.Format(_localizedText[key], args);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
                string debugOutput = string.Empty;

                foreach (var arg in args)
                {
                    debugOutput += arg + ",";
                }

                var substring = debugOutput.Substring(0, debugOutput.Length - 1);
                UnityEngine.Debug.LogError("LocalizeText の引数と GetText の引数の数が一致していないです。引数の数を一致させてください。");
                UnityEngine.Debug.LogError(_localizedText[key]);
                UnityEngine.Debug.LogError(substring);
                throw;
            }

            return value;
        }

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
            var keyValueLine = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
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

                var content = keyValue[languageIndex].Replace("\\n", Environment.NewLine);

                dic.Add(keyValue[0], content);
            }

            return new ReadOnlyDictionary<string, string>(dic);
        }
    }
}