// Auto Generated File
// Menu : Tools -> Icarus -> LocalizationEnumGenerate
using UnityEngine;
using UnityEngine.UI;

namespace Icarus.Core
{
    [RequireComponent(typeof(Text))]
    public class IcarusUGUITextLocalize : MonoBehaviour
    {
        [SerializeField] private string _key;
        private void Start()
        {
            var textComponent = this.GetComponent<Text>();
            textComponent.text = TextLocalizer.GetText(_key);
        }
    }
}
