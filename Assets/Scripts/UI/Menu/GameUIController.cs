using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _mainUI;

    public void EnableGameUI()
    {
        _gameUI.SetActive(true);
        if (_mainUI != null)
        {
            _mainUI.SetActive(false);
        }
    }

    public void DisableGameUI()
    {
        _gameUI.SetActive(false);
        if (_mainUI != null)
        {
            _mainUI.SetActive(true);
        }
    }
}
