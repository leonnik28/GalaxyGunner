using UnityEngine;
using UnityEngine.UI;

public class UIElementSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioObject;

    private Button _sourceButton;

    private void Awake()
    {
        _sourceButton = GetComponent<Button>();
        _sourceButton.onClick.AddListener(OnPointerClick);
    }

    private void OnPointerClick()
    {
        _audioObject.PlayOneShot(_audioObject.clip);
    }
}
