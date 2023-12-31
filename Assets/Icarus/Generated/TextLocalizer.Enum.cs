// Auto Generated File
// Menu : Tools -> Icarus -> LocalizationEnumGenerate

namespace Icarus.Core
{
    public static partial class TextLocalizer
    {
        public static string GetText(LocalizationEnum enumKey)
        {
            var key = enumKey.ToString();
            return GetText(key);
        }

        public static string GetText(LocalizationEnum enumKey, params object[] args)
        {
            var key = enumKey.ToString();
            return GetText(key, args);
        }
    }
}
