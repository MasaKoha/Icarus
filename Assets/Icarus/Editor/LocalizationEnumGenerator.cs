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
        [MenuItem("Tools/LocalizationEnumGenerate")]
        private static void Generate()
        {
            var text = FileLoader.Load().text;

            var keys = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split(',').First())
                .Where(key => key != "key");

            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> LocalizationEnumGenerate");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine("    public enum LocalizationEnum");
            builder.AppendLine("    {");
            foreach (var k in keys)
            {
                builder.Append("        ").Append(k).AppendLine(",");
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");

            // Icarusが置いてあるフォルダのGeneratedに生成する
            var targetFolderName = System.IO.Path.DirectorySeparatorChar + "Icarus";
            var icarusPath = GetTargetFolderPath(targetFolderName, "Assets");

            if (!string.IsNullOrEmpty(icarusPath))
            {
                var generateTargetDir = Path.Combine(icarusPath, "Generated");
                if (!Directory.Exists(generateTargetDir))
                {
                    Directory.CreateDirectory(generateTargetDir);
                }

                var generatedPath = Path.Combine(generateTargetDir, "LocalizationEnum.cs");
                using (var writer = new StreamWriter(generatedPath))
                {
                    writer.Write(builder.ToString());
                    writer.Flush();
                }

                AssetDatabase.Refresh();
                Debug.Log("icarus generated the LocalizationEnum.cs file at:" + generatedPath);
            }
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