using UnityEngine;

namespace Icarus.Core
{
    public static class FileLoader
    {
        private const string TextPath = "LocalizeText";

        public static TextAsset Load() => Resources.Load<TextAsset>(TextPath);
    }
}