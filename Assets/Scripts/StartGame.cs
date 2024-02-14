using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class StartGame : MonoBehaviour
{
    public event Action OnGameStart;

    [SerializeField] private Player _player;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private GunSpawn _gunSpawn;

    [Space]
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _movementUI;
    [SerializeField] private CinemachineVirtualCamera _virtualCameraUI;

    [Space]
    [SerializeField] private GameObject _screen;
    [SerializeField] private Material _screenMaterial;

    [Space]
    [SerializeField] private int _timeDelay;

    private InputSystemUIInputModule _mainUiInputSystemModule;

    private void Start()
    {
        _mainUiInputSystemModule = _mainUI.GetComponent<InputSystemUIInputModule>();
    }

    private void OnEnable()
    {
        OnGameStart += _player.GameStart;
        OnGameStart += _shooting.GameStart;
    }

    private void OnDisable()
    {
        OnGameStart -= _player.GameStart;
        OnGameStart -= _shooting.GameStart;
    }

    public async void PlayGame()
    {
        if (_mainUiInputSystemModule.enabled) {
            _mainUiInputSystemModule.enabled = false;
            await Task.Delay(1000);
            _mainUI.SetActive(false);
            _virtualCameraUI.gameObject.SetActive(false);
            _movementUI.SetActive(true);

            ChangeScreenMaterial();
            _gunSpawn.Spawn();
            StartMove();
            _mainUiInputSystemModule.enabled = true;
        }
    }

    private void ChangeScreenMaterial()
    {
        _screen.GetComponent<MeshRenderer>().material = _screenMaterial;
    }

    private async void StartMove()
    {
        await Task.Delay(_timeDelay);
        OnGameStart?.Invoke();
    }
}
