using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private GameObject _pauseUI;

    public void GameStop()
    {
        _updateGameLevel.StopGameAction();

        _pauseUI.SetActive(true);

        _updateGameLevel.SetMusic();

        Time.timeScale = 0;
    }

    public void Resume()
    {
        _pauseUI.SetActive(false);

        _updateGameLevel.StartGameAction();
    }

    public void Home()
    {
        _pauseUI.SetActive(false);
        _updateGameLevel.GameOver(false);
    }
}
