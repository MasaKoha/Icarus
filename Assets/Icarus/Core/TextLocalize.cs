using UnityEngine;
using UnityEngine.UI;

namespace Icarus.Core
{
    [RequireComponent(typeof(Text))]
    public class TextLocalize : MonoBehaviour
    {
        [SerializeField] private string _key;

        private void Start()
        {
            var textComponent = this.GetComponent<Text>();
            textComponent.text = TextLocalizer.LocalizedText[_key];
        }
    }
}