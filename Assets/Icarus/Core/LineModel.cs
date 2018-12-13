using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Icarus.Core
{
    public class LineModel
    {
        public readonly string Key;
        public readonly string Value;

        public LineModel(int defaultLanguageIndex, int targetLanguageIndex, string[] values)
        {
            Key = values[0];
            var index = targetLanguageIndex;
            if (values.Length <= targetLanguageIndex)
            {
                index = defaultLanguageIndex;
            }

            if (values[index] == string.Empty)
            {
                index = defaultLanguageIndex;
            }

            Value = values[index].Replace("\\n", Environment.NewLine);
        }
    }
}