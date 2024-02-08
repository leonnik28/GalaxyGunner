using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameUI;

    public void EnableGameUI()
    {
        _gameUI.SetActive(true);
    }

    public void DisableGameUI()
    {
        _gameUI.SetActive(false);
    }
}
