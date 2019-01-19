using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Icarus.Editor
{
    public static class ScriptGenerator
    {
        public static string GenerateTextLocalizer(string enumScriptName)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> LocalizationEnumGenerate");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine("    public static partial class TextLocalizer");
            builder.AppendLine("    {");
            builder.AppendLine($"        public static string GetText({enumScriptName} enumKey)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            var key = enumKey.ToString();");
            builder.AppendLine($"            return GetText(key);");
            builder.AppendLine($"        }}");
            builder.AppendLine("");
            builder.AppendLine($"        public static string GetText({enumScriptName} enumKey, params object[] args)");
            builder.AppendLine($"        {{");
            builder.AppendLine($"            var key = enumKey.ToString();");
            builder.AppendLine($"            return GetText(key, args);");
            builder.AppendLine($"        }}");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static string GenerateEnumScript(LocalizedTextLineModel[] liveModels, LocalizedTextHeaderModel header, string enumName)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> LocalizationEnumGenerate");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine($"    public enum {enumName}");
            builder.AppendLine("    {");

            for (int i = 0; i < liveModels.Length; i++)
            {
                var line = liveModels[i];
                if (line.Type == LocalizedTextLineType.KeyValue)
                {
                    builder.AppendLine($"        /// <summary>");
                    for (int j = 0; j < line.LocalizedTexts.Length; j++)
                    {
                        builder.AppendLine($"        /// {header.Languages[j]} : {line.LocalizedTexts[j]}");
                    }
                    builder.AppendLine($"        /// </summary>");
                    builder.AppendLine($"        {line.Key},");
                    continue;
                }

                if (line.Type == LocalizedTextLineType.Comment)
                {
                    builder.AppendLine($"        {line.LineText}");
                    continue;
                }
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static string GenerateUGUITextLocalize(string enumName)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// Auto Generated File");
            builder.AppendLine("// Menu : Tools -> LocalizationEnumGenerate");
            builder.AppendLine("using UnityEngine;");
            builder.AppendLine("using UnityEngine.UI;");
            builder.AppendLine("namespace Icarus.Core");
            builder.AppendLine("{");
            builder.AppendLine("    [RequireComponent(typeof(Text))]");
            builder.AppendLine("    public class UGUITextLocalize : MonoBehaviour");
            builder.AppendLine("    {");
            builder.AppendLine($"        [SerializeField] private {enumName} _key;");
            builder.AppendLine("        private void Start()");
            builder.AppendLine("        {");
            builder.AppendLine("            var textComponent = this.GetComponent<Text>();");
            builder.AppendLine("            textComponent.text = TextLocalizer.GetText(_key);");
            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}