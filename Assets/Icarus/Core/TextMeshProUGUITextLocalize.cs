using TMPro;
using UnityEngine;

namespace Icarus.Core
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProUGUITextLocalize : MonoBehaviour
    {
        [SerializeField] private string _key;

        private void Start()
        {
            var textComponent = this.GetComponent<TextMeshProUGUI>();
            textComponent.text = TextLocalizer.GetText(_key);
        }
    }
}