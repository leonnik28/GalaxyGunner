using Cinemachine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UpdateGameLevel : MonoBehaviour
{
    public bool IsGameActive => _isGameActive;

    public event Action <int> OnChangeTopScore;

    [SerializeField] private List<Chunk> _emptyChunksList;

    [Header("Game Objects")]
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Material _screenMaterial;
    [SerializeField] private GameObject _joystick;

    [Header("Gameplay Components")]
    [SerializeField] private RoadGenerate _roadGenerate;
    [SerializeField] private CinemachineVirtualCamera _uiVirtualCamera;
    [SerializeField] private GunSpawn _gunSpawn;
    [SerializeField] private Score _score;

    [Header("Player Controls")]
    [SerializeField] private Player _player;
    [SerializeField] private Movement _movement;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Animator _runAnimator;

    [Header("Services Components")]
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private TopScore _topScore;
    [SerializeField] private Credits _credits;
    [SerializeField] private Achievements _achievements;

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
    [SerializeField] private int _creditsFactor = 21;

    private Vector3 _updatedPlayerPosition;
    private bool _isGameActive = true;
    private bool _isMusicMute;

    private void Start()
    {
        _updatedPlayerPosition = _model.transform.position;
    }

    public void GameOver(bool isReload = true)
    {
        _isGameActive = false;
        _joystick.gameObject.SetActive(false);
        _movementUI.SetActive(false);

        if (_playerHealthIndex >= 1 && isReload)
        {
            _playerHealthIndex--;
            _reliveUI.SetActive(true);
        }
        else
        {
            OpenDeahtUI();
        }

        SetMusic();

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

        if (!_isMusicMute)
        {
            _player.SetMusic(true, false);
        }

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

        if (_score.CurrentScore > _topScore.CurrentTopScore)
        {
            ChangeTopScore(currentChangedCredits);
        }
        else
        {
            _credits.ChangeCredits(currentChangedCredits, true);
        }
    }

    private void SetMusic()
    {
        if (_player.GameMusic.mute)
        {
            _isMusicMute = true;
        }
        else
        {
            _isMusicMute = false;
            _player.SetMusic(false, false);
        }
    }

    private async void GameReset()
    {
        await Task.Delay(_gameResetDelay);
        _isGameActive = true;
    }

    private void UpdateWorld()
    {
        _player.GameStop();
        if (!_isMusicMute)
        {
            _player.SetMusic(false);
        }
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

    private async void ChangeTopScore(int currentCredits)
    {
        _credits.ChangeCredits(currentCredits);
        await _gameSession.SaveGame(credits: _credits.CurrentCredits, topScore: _score.CurrentScore);

        if (_gameSession.OnLoginToGoogleGames)
        {
            OnChangeTopScore?.Invoke(_score.CurrentScore);
            if (_score.CurrentScore >= 10000)
            {
                string achievementId = "CgkIyvTP6NIPEAIQBA";
                _achievements.UpdateAchivement(achievementId);
            }
        }
    }

    private void ChangeScreenMaterial()
    {
        _screen.GetComponent<MeshRenderer>().material = _screenMaterial;
    }
}
