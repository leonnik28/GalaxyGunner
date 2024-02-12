using UnityEngine;
using TMPro;

namespace Michsky.UI.Shift
{
    [ExecuteInEditMode]
    public class MainButton : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _buttonText = "";
        [SerializeField] private bool _useCustomText = false;

        [Header("Resources")]
        public TextMeshProUGUI normalText;
        public TextMeshProUGUI highlightedText;
        public TextMeshProUGUI pressedText;

        void OnEnable()
        {
            if (_useCustomText == false)
            {
                if (normalText != null) { normalText.text = _buttonText; }
                if (highlightedText != null) { highlightedText.text = _buttonText; }
                if (pressedText != null) { pressedText.text = _buttonText; }
            }
        }
    }
}