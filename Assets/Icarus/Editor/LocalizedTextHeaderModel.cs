using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Icarus.Editor
{
    public class LocalizedTextHeaderModel
    {
        public readonly string[] Languages;
        public LocalizedTextHeaderModel(string header)
        {
            Languages = header.Split(',').Skip(1).ToArray();
        }
    }

}