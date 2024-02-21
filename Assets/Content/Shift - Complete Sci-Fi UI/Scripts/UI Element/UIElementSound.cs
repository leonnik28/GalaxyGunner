using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Michsky.UI.Shift
{
    [ExecuteInEditMode]
    public class UIElementSound : MonoBehaviour, IPointerClickHandler
    {
        [Header("Resources")]
        [SerializeField] private UIManager _uIManagerAsset;
        [SerializeField] private AudioSource _audioObject;

        [Header("Custom SFX")]
        public AudioClip hoverSFX;
        public AudioClip clickSFX;

        [Header("Settings")]
        public bool enableHoverSound = true;
        public bool enableClickSound = true;
        public bool checkForInteraction = true;

        private Button sourceButton;

        void OnEnable()
        {
            if (_uIManagerAsset == null)
            {
                _uIManagerAsset = Resources.Load<UIManager>("Shift UI Manager");
            }

            if (Application.isPlaying == true && _audioObject == null)
            {
              //  audioObject = GameObject.Find("UI Audio").GetComponent<AudioSource>();
            }

            if (checkForInteraction == true) { sourceButton = gameObject.GetComponent<Button>(); }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (checkForInteraction == true && sourceButton != null && sourceButton.interactable == false)
                return;

            if (enableClickSound == true)
            {
                if (clickSFX == null) { _audioObject.PlayOneShot(_uIManagerAsset.clickSound); }
                else { _audioObject.PlayOneShot(clickSFX); }
            }
        }
    }
}