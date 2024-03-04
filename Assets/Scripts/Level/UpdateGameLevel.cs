using Cinemachine;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UpdateGameLevel : MonoBehaviour
{
    public bool IsGameActive => _isGameActive;

    [SerializeField] private List<Chunk> _emptyChunksList;

    [Header("Game Objects")]
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Material _screenMaterial;

    [Header("Game Components")]
    [SerializeField] private RoadGenerate _roadGenerate;
    [SerializeField] private Player _player;
    [SerializeField] private Movement _movement;
    [SerializeField] private CinemachineVirtualCamera _uiVirtualCamera;
    [SerializeField] private GunSpawn _gunSpawn;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Animator _runAnimator;

    [Header("UI Elements")]
    [SerializeField] private GameObject _deathUI;
    [SerializeField] private GameObject _reliveUI;
    [SerializeField] private GameObject _movementUI;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _finalCreditText;

    [Header("Game Settings")]
    [SerializeField] private int _playerHealthIndex = 1;
    [SerializeField] private int _emptyChunkDeletionTime = 3000;
    [SerializeField] private int _gameResetDelay = 200;
    [SerializeField] private Score _score;
    [SerializeField] private Credits _credits;

    private Vector3 _updatedPlayerPosition;
    private bool _isGameActive = true;
    private int _creditsFactor = 10;

    private InputSystemUIInputModule _deathUiInputSystemModule;

    private void Start()
    {
        _updatedPlayerPosition = _model.transform.position;
        _deathUiInputSystemModule = _deathUI.GetComponent<InputSystemUIInputModule>();
    }

    public void GameOver()
    {
        _isGameActive = false;
        _movementUI.SetActive(false);

        if (_playerHealthIndex >= 1)
        {
            _playerHealthIndex--;
            _reliveUI.SetActive(true);
        }
        else
        {
            OpenDeahtUI();
        }

        _player.SetMusic(false, false);
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
        _player.SetMusic(true, false);
        Time.timeScale = 1;

        await Task.Delay(_emptyChunkDeletionTime);
        if (emptyChunk)
        {
            emptyChunk.gameObject.SetActive(false);
        }
    }

    public void ExitReliveUI()
    {
        _reliveUI.SetActive(false);
        OpenDeahtUI();
    }

    public void ExitDeathUI()
    {
        _deathUI.SetActive(false);

        UpdateWorld();
        _mainUI.SetActive(true);
    }

    private void OpenDeahtUI()
    {
        _deathUI.SetActive(true);
        int currentChangedCredits = _score.CurrentScore / _creditsFactor;

        _finalScoreText.text = "Score: " + _score.CurrentScore.ToString();
        _finalCreditText.text = "Credits: " + currentChangedCredits.ToString();

        _credits.ChangeCredits(currentChangedCredits, true);
    }

    private async void GameReset()
    {
        await Task.Delay(_gameResetDelay);
        _isGameActive = true;
    }

    private void UpdateWorld()
    {
        _player.GameStop();
        _roadGenerate.StartGame();
        _gunSpawn.DeleteGun();
        _shooting.GameStop();

        _movement.ResetMovement(_updatedPlayerPosition);
        _runAnimator.Play("StayHandsAnimation");

        ChangeScreenMaterial();
        _uiVirtualCamera.gameObject.SetActive(true);

        GameReset();

        _playerHealthIndex = 1;
        Time.timeScale = 1;
    }

    private void ChangeScreenMaterial()
    {
        _screen.GetComponent<MeshRenderer>().material = _screenMaterial;
    }
}
