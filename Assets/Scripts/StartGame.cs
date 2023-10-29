using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _aimExtern;
    [SerializeField] private GameObject _aimInternal;

    private bool _isGameActive = false;

    private void Start()
    {
    }

    public void StartLevel()
    {
        _isGameActive = true;
        _aimExtern.SetActive(true);
        _aimInternal.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void StopGame()
    {
        _aimExtern.SetActive(false);
        _aimInternal.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (_isGameActive)
        {
            _aimExtern.SetActive(true);
            _aimInternal.SetActive(true);
        }
        Time.timeScale = 1;
    }
}
