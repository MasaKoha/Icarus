using UnityEditor;
using Icarus.Core;
using System.Linq;
using System;
using System.IO;
using System.Text;

namespace Icarus.Editor
{
    public static class LocalizationEnumGenerator
    {
        private const string ExportFilePath = "Assets/Icarus/Core/LocalizationEnum.cs";

        [MenuItem("Tools/LocalizationEnumGenerate")]
        private static void Generate()
        {
            var text = FileLoader.Load().text;

            var keys = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                        .Where(line => !line.Contains("//"))
                        .Where(line => !string.IsNullOrWhiteSpace(line))
                        .Select(line => line.Split(',').First())
                        .Where(key => key != "key");

            var builder = new StringBuilder();
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

            using (var writer = new StreamWriter(ExportFilePath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }
            AssetDatabase.Refresh();
        }
    }
}