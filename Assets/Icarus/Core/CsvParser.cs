using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Icarus.Core
{
    internal static class CsvParser
    {
        private const string RegexPattern = @"((?<=^|,)(?:""(?<field>(?:[^""]|"""")*)""|(?<field>[^,]*))(?=,|$))";

        internal static List<string> Parse(string line)
        {
            var matches = Regex.Matches(line, RegexPattern);
            var fields = new List<string>();

            foreach (Match match in matches)
            {
                var field = match.Value;
                if (field.StartsWith("\"") && field.EndsWith("\""))
                {
                    field = field.Substring(1, field.Length - 2);
                    field = field.Replace("\"\"", "\"");
                }

                fields.Add(field);
            }

            return fields;
        }
    }
}