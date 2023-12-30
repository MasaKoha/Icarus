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