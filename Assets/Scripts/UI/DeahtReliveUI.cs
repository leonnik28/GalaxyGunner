using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DeahtReliveUI : MonoBehaviour
{
    [SerializeField] private RoadGenerate _roadGenerate;
    [SerializeField] private Chunk _chunk;
    [SerializeField] private GameObject _model;
    [SerializeField] private Player _player;
    [SerializeField] private Movement _movement;
    [SerializeField] private CinemachineVirtualCamera _vcamUI;
    [SerializeField] private GunSpawn _gunSpawn;

    [Space]
    [SerializeField] private GameObject _screen;
    [SerializeField] private Material _screenMaterial;

    [SerializeField] private GameObject _deathUI;
    [SerializeField] private GameObject _reliveUI;
    [SerializeField] private GameObject _movementUI;
    [SerializeField] private GameObject _mainUI;

    [SerializeField] private int _healthIndex = 1;

    private int _timeDeleteEmptyChunk = 5000;
    private Vector3 _playerUpdated;

    private void Start()
    {
        _playerUpdated = _model.transform.position;
    }

    public void GameOver()
    {
        if (_healthIndex >= 1)
        {
            _healthIndex--;
            _movementUI.SetActive(false);
            _reliveUI.SetActive(true);
        }
        else
        {
            _movementUI.SetActive(false);
            _deathUI.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public async void Relive()
    {
        _chunk = Instantiate(_chunk, transform);
        _roadGenerate.ChangeChunk(_chunk);

        _reliveUI.SetActive(false);
        _movementUI.SetActive(true);

        Time.timeScale = 1;

        await Task.Delay(_timeDeleteEmptyChunk);
        _chunk.gameObject.SetActive(false);
    }

    public void ExitUI()
    {
        _deathUI?.SetActive(false);
        _reliveUI?.SetActive(false);

        _mainUI.SetActive(true);
        UpdateWorld();
    }

    private void UpdateWorld()
    {
        _player.GameStop();
        _roadGenerate.StartGame();
        ChangeScreenMaterial();
        _movement.ResetMovement(_playerUpdated);
        _vcamUI.gameObject.SetActive(true);
        _gunSpawn.DeleteGun();
        Time.timeScale = 1;
    }

    private void ChangeScreenMaterial()
    {
        _screen.GetComponent<MeshRenderer>().material = _screenMaterial;
    }
}
