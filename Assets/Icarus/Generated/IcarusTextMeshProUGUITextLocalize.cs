// Auto Generated File
// Menu : Tools -> Icarus -> LocalizationEnumGenerate
using UnityEngine;
using TMPro;

namespace Icarus.Core
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class IcarusTextMeshProUGUITextLocalize : MonoBehaviour
    {
        [SerializeField] private string _key;
        private void Start()
        {
            var textComponent = this.GetComponent<TextMeshProUGUI>();
            textComponent.text = TextLocalizer.GetText(_key);
        }
    }
}
