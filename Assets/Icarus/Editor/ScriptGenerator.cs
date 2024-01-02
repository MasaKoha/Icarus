using System;
using System.Text;

namespace Icarus.Editor
{
    public static class ScriptGenerator
    {
        public static string GenerateTextLocalizer(string enumScriptName)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> Icarus -> LocalizationEnumGenerate");
            builder.AppendLine("");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine("    public static partial class TextLocalizer");
            builder.AppendLine("    {");
            builder.AppendLine($"        public static string GetText({enumScriptName} enumKey)");
            builder.AppendLine("        {");
            builder.AppendLine("            var key = enumKey.ToString();");
            builder.AppendLine("            return GetText(key);");
            builder.AppendLine("        }");
            builder.AppendLine("");
            builder.AppendLine($"        public static string GetText({enumScriptName} enumKey, params object[] args)");
            builder.AppendLine("        {");
            builder.AppendLine("            var key = enumKey.ToString();");
            builder.AppendLine("            return GetText(key, args);");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static string GenerateEnumScript(LocalizedTextLineModel[] liveModels, LocalizedTextHeaderModel header, string enumName)
        {
            var commentCount = 0;
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> Icarus -> LocalizationEnumGenerate");
            builder.AppendLine("");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine($"    public enum {enumName}");
            builder.AppendLine("    {");
            var existComment = false;

            foreach (var line in liveModels)
            {
                if (line.Type == LocalizedTextLineType.KeyValue)
                {
                    for (var j = 0; j < line.LocalizedTexts.Length; j++)
                    {
                        builder.AppendLine($"        // {header.Languages[j]} : {line.LocalizedTexts[j]}");
                    }

                    builder.AppendLine($"        {line.Key},");
                    builder.AppendLine("");
                    continue;
                }

                if (line.Type == LocalizedTextLineType.Comment)
                {
                    continue;
                }

                if (line.Type == LocalizedTextLineType.Category)
                {
                    if (commentCount != 0)
                    {
                        if (existComment)
                        {
                            builder.AppendLine($"        #endregion{Environment.NewLine}");
                        }
                    }

                    // Remove "//" in Comment
                    builder.AppendLine($"        #region {line.LineText.Remove(0, 2)}");
                    existComment = true;
                    continue;
                }

                commentCount++;
            }

            if (existComment)
            {
                builder.AppendLine("        #endregion");
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static string GenerateUGUITextLocalize()
        {
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> Icarus -> LocalizationEnumGenerate");
            builder.AppendLine("using UnityEngine;");
            builder.AppendLine("using UnityEngine.UI;");
            builder.AppendLine("");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine("    [RequireComponent(typeof(Text))]");
            builder.AppendLine("    public class IcarusUGUITextLocalize : MonoBehaviour");
            builder.AppendLine("    {");
            builder.AppendLine("        [SerializeField] private string _key;");
            builder.AppendLine("        private void Start()");
            builder.AppendLine("        {");
            builder.AppendLine("            var textComponent = this.GetComponent<Text>();");
            builder.AppendLine("            textComponent.text = TextLocalizer.GetText(_key);");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static string GenerateTextMeshProUGUITextLocalize()
        {
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> Icarus -> LocalizationEnumGenerate");
            builder.AppendLine("using UnityEngine;");
            builder.AppendLine("using TMPro;");
            builder.AppendLine("");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine("    [RequireComponent(typeof(TextMeshProUGUI))]");
            builder.AppendLine("    public class IcarusTextMeshProUGUITextLocalize : MonoBehaviour");
            builder.AppendLine("    {");
            builder.AppendLine("        [SerializeField] private string _key;");
            builder.AppendLine("        private void Start()");
            builder.AppendLine("        {");
            builder.AppendLine("            var textComponent = this.GetComponent<TextMeshProUGUI>();");
            builder.AppendLine("            textComponent.text = TextLocalizer.GetText(_key);");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}