using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Icarus.Core
{
    public static partial class TextLocalizer
    {
        private static ReadOnlyDictionary<string, string> _localizedText;

        public static void Initialize(string defaultLanguage, string targetLanguage)
        {
            _localizedText = GetLocalizedText(defaultLanguage, targetLanguage, FileLoader.LoadDefaultFile().text);
        }

        public static void Initialize(string defaultLanguage, string targetLanguage, string targetRawText)
        {
            _localizedText = GetLocalizedText(defaultLanguage, targetLanguage, targetRawText);
        }

        public static string GetText(string key)
        {
            _localizedText.TryGetValue(key, out var text);
            if (text == null)
            {
                throw new KeyNotFoundException($"Key Not Found : {key}");
            }

            return text;
        }

        public static string GetText(string key, params object[] args)
        {
            string value;
            try
            {
                value = string.Format(_localizedText[key], args);
            }
            catch (FormatException)
            {
                var debugOutput = string.Empty;

                foreach (var arg in args)
                {
                    debugOutput += arg + ",";
                }

                var substring = debugOutput.Substring(0, debugOutput.Length - 1);
                throw new FormatException($"Body の可変長引数に対して、param の可変長引数が足りない Key : {key} , Body : {_localizedText[key]} , params : {substring}");
            }

            return value;
        }

        private static ReadOnlyDictionary<string, string> GetLocalizedText(string defaultLanguage, string targetLanguage, string text)
        {
            var dic = new Dictionary<string, string>();
            var keyValueLine = text.Split(new[] { "\n", "\r", "\r\n" }, StringSplitOptions.None);
            var langAttribute = keyValueLine[0].Split(',');
            var defaultLanguageIndex = 0;
            var targetLanguageIndex = 0;

            for (var i = 0; i < langAttribute.Length; i++)
            {
                if (defaultLanguage == langAttribute[i])
                {
                    defaultLanguageIndex = i;
                }

                if (targetLanguage == langAttribute[i])
                {
                    targetLanguageIndex = i;
                }
            }

            if (defaultLanguageIndex == 0)
            {
                throw new Exception($"Language \"{defaultLanguage}\" not Exists");
            }

            if (targetLanguageIndex == 0)
            {
                throw new Exception($"Language \"{targetLanguage}\" not Exists");
            }

            for (var rawTextLine = 1; rawTextLine < keyValueLine.Length; rawTextLine++)
            {
                // "//" : Comment out
                if (keyValueLine[rawTextLine].StartsWith("//"))
                {
                    continue;
                }

                // keyValue.Length が 1 以下だと、正しく設定されていないもしくは空白行部分なので避ける
                if (keyValueLine[rawTextLine].Split(',').Length <= 1)
                {
                    continue;
                }

                var lineModel = new LineModel(defaultLanguageIndex, targetLanguageIndex, keyValueLine[rawTextLine].Split(','));
                dic.Add(lineModel.Key, lineModel.Value);
            }

            return new ReadOnlyDictionary<string, string>(dic);
        }
    }
}