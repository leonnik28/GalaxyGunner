using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private CinemachineVirtualCamera _uiVcam;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _uiMovement;
    [SerializeField] private int _timeDelay;

    public void PlayGame()
    {
        _mainUI.SetActive(false);
        _uiVcam.gameObject.SetActive(false);
        _uiMovement.SetActive(true);
        StartMove();
    }

    private async void StartMove()
    {
        await Task.Delay(_timeDelay);
        _player.GameStart();
    }
}
