using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Player _player;
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

    public void PlayGame()
    {
        _mainUI.SetActive(false);

        ChangeScreenMaterial();

        _virtualCameraUI.gameObject.SetActive(false);

        _movementUI.SetActive(true);

        _gunSpawn.Spawn();
        
        StartMove();
    }

    private void ChangeScreenMaterial()
    {
        _screen.GetComponent<MeshRenderer>().material = _screenMaterial;
    }

    private async void StartMove()
    {
        await Task.Delay(_timeDelay);
        _player.GameStart();
    }
}
