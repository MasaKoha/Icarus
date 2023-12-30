using UnityEngine;
using System.IO;

namespace Icarus.Core
{
    public static class FileLoader
    {
        private const string TextPath = "IcarusLocalizedText";

        public static TextAsset LoadDefaultFile() => Resources.Load<TextAsset>(TextPath);

        public static string LoadFile(string filePath)
        {
            using var sr = new StreamReader(filePath);
            var text = sr.ReadToEnd();
            return text;
        }
    }
}