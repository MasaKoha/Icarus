using Icarus.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Icarus.Example
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Text _text1;
        [SerializeField] private Text _text2;

        private void Awake()
        {
            TextLocalizer.Initialize("ja", "ja");
            var text1 = TextLocalizer.GetText(LocalizationEnum.KeyTest);
            var text2 = TextLocalizer.GetText("KeyTest");
            _text1.text = text1;
            _text2.text = text2;
        }
    }
}