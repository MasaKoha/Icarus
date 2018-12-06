using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Icarus.Core;
using UnityEditor;
using UnityEngine;

namespace Icarus.Editor
{
    public static class LocalizationEnumGenerator
    {
        private const string DefaultEnumName = "LocalizationEnum";

        private const string DefaultTextLocalizerFileName = "TextLocalizer.Enum";

        private const string UGUITextLocalizeFileName = "UGUITextLocalize";

        [MenuItem("Tools/LocalizationEnumGenerate")]
        public static void Generate()
        {
            var text = FileLoader.LoadDefaultFile().text;
            var lineModels = CreateLocalizedTextModel(text);
            var duplicatedKeys = GetDuplicatedKeys(lineModels);

            if (duplicatedKeys.Count >= 1)
            {
                string failedKeys = "{";
                foreach (var key in duplicatedKeys)
                {
                    failedKeys = failedKeys + key + ",";
                }
                failedKeys += "}";
                throw new Exception($"Duplicated Key {failedKeys}");
            }

            var header = new LocalizedTextHeaderModel(lineModels[0].LineText);

            var enumCode = ScriptGenerator.GenerateEnumScript(lineModels, header, DefaultEnumName);
            var generatedEnumScriptPath = TargetDefaultOutputFilePath(DefaultEnumName);
            GenerateEnumFile(enumCode, generatedEnumScriptPath);

            var localizerCode = ScriptGenerator.GenerateTextLocalizer(DefaultEnumName);
            var generatedLocalizerScriptPath2 = TargetDefaultOutputFilePath(DefaultTextLocalizerFileName);
            GenerateEnumFile(localizerCode, generatedLocalizerScriptPath2);

            var uguiTextLocalize = ScriptGenerator.GenerateUGUITextLocalize(DefaultEnumName);
            var generatedUguiTextLocalize = TargetDefaultOutputFilePath(UGUITextLocalizeFileName);
            GenerateEnumFile(uguiTextLocalize, generatedUguiTextLocalize);

            AssetDatabase.Refresh();
        }

        public static void Generate(string targetLocalizedText, string generatedPath)
        {
            var lineModels = CreateLocalizedTextModel(targetLocalizedText);
            var duplicatedKeys = GetDuplicatedKeys(lineModels);

            if (duplicatedKeys.Count >= 1)
            {
                string failedKeys = "{";
                foreach (var key in duplicatedKeys)
                {
                    failedKeys = failedKeys + key + ",";
                }
                failedKeys += "}";
                throw new Exception($"Duplicated Key {failedKeys}");
            }

            var header = new LocalizedTextHeaderModel(lineModels[0].LineText);
            var code = ScriptGenerator.GenerateEnumScript(lineModels, header, DefaultEnumName);
            GenerateEnumFile(code, generatedPath);
            AssetDatabase.Refresh();
        }

        private static List<string> GetDuplicatedKeys(LocalizedTextLineModel[] models)
        {
            return models.Where(line => line.Type == LocalizedTextLineType.KeyValue)
                .GroupBy(line => line.Key)
                .Where(groupedKey => groupedKey.Count() > 1)
                .Select(groupedKey => groupedKey.Key).ToList();
        }

        public static LocalizedTextLineModel[] CreateLocalizedTextModel(string text)
        {
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var lineModels = new LocalizedTextLineModel[lines.Length];
            for (int i = 0; i < lineModels.Length; i++)
            {
                lineModels[i] = new LocalizedTextLineModel(i, lines[i]);
            }

            return lineModels;
        }

        private static void GenerateEnumFile(string scriptText, string generatedPath)
        {
            using (var writer = new StreamWriter(generatedPath))
            {
                writer.Write(scriptText);
                writer.Flush();
            }

            Debug.Log("Icarus generated the LocalizationEnum.cs file at : " + generatedPath);
        }

        private static string TargetDefaultOutputFilePath(string fileName)
        {
            // Icarusが置いてあるフォルダのGeneratedに生成する
            var targetFolderName = System.IO.Path.DirectorySeparatorChar + "Icarus";
            var icarusPath = GetTargetFolderPath(targetFolderName, "Assets");

            var generatedPath = "";
            if (!string.IsNullOrEmpty(icarusPath))
            {
                var generateTargetDir = Path.Combine(icarusPath, "Generated");
                if (!Directory.Exists(generateTargetDir))
                {
                    Directory.CreateDirectory(generateTargetDir);
                }

                generatedPath = Path.Combine(generateTargetDir, $"{fileName}.cs");
            }
            return generatedPath;
        }

        private static string GetTargetFolderPath(string targetFolderName, string findStartPath)
        {
            var childDirectoryPaths = Directory.GetDirectories(findStartPath);

            return Recursive(targetFolderName, childDirectoryPaths);
        }

        private static string Recursive(string targetFolderName, string[] paths)
        {
            foreach (var dirPath in paths)
            {
                // チェックを行う
                if (dirPath.Contains(targetFolderName))
                {
                    return dirPath;
                }

                // まだパスに欲しいフォルダ名が含まれていない。

                // 下の階層があるかチェック
                var child2 = Directory.GetDirectories(dirPath);
                if (child2.Length == 0)
                {
                    continue;
                }

                // 下の階層があるので、読み込み
                var result = Recursive(targetFolderName, child2);
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            return string.Empty;
        }
    }
}