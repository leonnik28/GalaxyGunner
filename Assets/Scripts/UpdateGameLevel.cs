using Cinemachine;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UpdateGameLevel : MonoBehaviour
{
    public bool GameActive => _gameActive;

    [SerializeField] private List<Chunk> _emptyChunksList;

    [Header("Active Game Objects")]
    [SerializeField] private RoadGenerate _roadGenerate;
    [SerializeField] private GameObject _model;
    [SerializeField] private Player _player;
    [SerializeField] private Movement _movement;
    [SerializeField] private CinemachineVirtualCamera _vcamUI;
    [SerializeField] private GunSpawn _gunSpawn;
    [SerializeField] private Animator _runAnimator;

    [Space]
    [SerializeField] private GameObject _screen;
    [SerializeField] private Material _screenMaterial;

    [Header("Active UI")]
    [SerializeField] private GameObject _deathUI;
    [SerializeField] private GameObject _reliveUI;
    [SerializeField] private GameObject _movementUI;
    [SerializeField] private GameObject _mainUI;

    [Header("Const Values")]
    [SerializeField] private int _healthIndex = 1;
    [SerializeField] private int _timeDeleteEmptyChunk = 3000;
    [SerializeField] private int _gameResetDelay = 1000;

    private Vector3 _playerUpdated;
    private bool _gameActive = true;

    private void Start()
    {
        _playerUpdated = _model.transform.position;
    }

    public void GameOver()
    {
        _gameActive = false;
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
        Chunk emptyChunk = _roadGenerate.FindNeedChunk(_emptyChunksList);
        emptyChunk = Instantiate(emptyChunk, transform);
        _roadGenerate.ChangeChunk(emptyChunk);

        _reliveUI.SetActive(false);
        _movementUI.SetActive(true);

        GameReset();
        Time.timeScale = 1;

        await Task.Delay(_timeDeleteEmptyChunk);
        emptyChunk.gameObject.SetActive(false);
    }

    public void ExitUI()
    {
        _deathUI?.SetActive(false);
        _reliveUI?.SetActive(false);

        UpdateWorld();
        _mainUI.SetActive(true);
    }

    private async void GameReset()
    {
        await Task.Delay(1000);
        _gameActive = true;
    }

    private void UpdateWorld()
    {
        _player.GameStop();
        _roadGenerate.StartGame();
        _gunSpawn.DeleteGun();

        _movement.ResetMovement(_playerUpdated);
        _runAnimator.Play("StayHandsAnimation");

        ChangeScreenMaterial();
        _vcamUI.gameObject.SetActive(true);

        GameReset();

        _healthIndex = 1;
        Time.timeScale = 1;
    }

    private void ChangeScreenMaterial()
    {
        _screen.GetComponent<MeshRenderer>().material = _screenMaterial;
    }
}
