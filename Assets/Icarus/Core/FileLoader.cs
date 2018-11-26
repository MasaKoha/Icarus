using UnityEngine;
using System.IO;
using System;

namespace Icarus.Core
{
    public static class FileLoader
    {
        private const string TextPath = "LocalizedText";

        public static TextAsset LoadDefaultFile() => Resources.Load<TextAsset>(TextPath);

        public static string LoadFile(string filePath)
        {
            var text = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
    }
}