using GooglePlayGames;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UserDataStorage;

public class GameSession : MonoBehaviour
{
    public bool OnLoginToGoogleGames => _onLoginToGoogleGames;

    public event Action<SaveData> OnUserDataLoaded;
    public event Action OnSuccessLogin;

    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _connectionUI;

    [SerializeField] private Achievements _achievements;
    [SerializeField] private StorageService _storageService;
    [SerializeField] private AdsInitializer _adsInitializer;

    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private Button _submitButton;

    private UserDataStorage _userDataStorage;
    private Avatars _avatars;
    private string _userId;
    private bool _onLoginToGoogleGames;

    private readonly string _saveDataName = "saveData";
    private readonly int _timeToDisable = 1000;

    private void Awake()
    {
        _storageService = new StorageService();

        _userDataStorage = gameObject.GetComponent<UserDataStorage>();
        _avatars = gameObject.GetComponent<Avatars>();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        _submitButton.onClick.AddListener(PromptForUsername);
    }

    private void Start()
    {
        Social.localUser.Authenticate(success =>
        {
            if (success == true)
            {
                _userId = PlayGamesPlatform.Instance.localUser.id;
                OnSuccessLogin?.Invoke();
                _onLoginToGoogleGames = true;

                LoadingUserDataFromCloud();
            }
            else
            {
                _onLoginToGoogleGames = false;

                LoadingUserDataFromLocalStorage();
            }
        });
    }

    public async Task SaveGame(int credits = 0, int topScore = 0, int avatarIndex = -1, int gunIndex = 0)
    {
        var saveData = await _storageService.LoadAsync<SaveData>(_saveDataName);
        if (string.IsNullOrEmpty(saveData.username))
        {
            return;
        }

        if (credits != 0)
        {
            saveData.credits = credits;
        }
        if (topScore != 0)
        {
            saveData.topScore = topScore;
        }
        if (avatarIndex != -1)
        {
            saveData.avatarIndex = avatarIndex;
        }
        if (gunIndex != 0)
        {
            saveData.gunIndex.Add(gunIndex);
        }

        await _storageService.SaveAsync(_saveDataName, saveData);
        OnUserDataLoaded?.Invoke(saveData);

        if (_onLoginToGoogleGames)
        {
            await _userDataStorage.SaveGame(saveData);
        }
    }

    public async Task SaveGame()
    {
        var saveData = await _storageService.LoadAsync<SaveData>(_saveDataName);
        await _userDataStorage.SaveGame(saveData);
    }

    public void LoginToGooglePlayGames()
    {
        Social.localUser.Authenticate(async success =>
        {
            if (success == true)
            {
                _userId = PlayGamesPlatform.Instance.localUser.id;
                OnSuccessLogin?.Invoke();
                _onLoginToGoogleGames = true;

                await SaveGame();
            }
            else
            {
                _onLoginToGoogleGames = false;
            }
        });
    }

    private async void LoadingUserDataFromCloud()
    {
        var saveData = await _userDataStorage.LoadGame(_userId);
        if (saveData.Equals(null) || string.IsNullOrEmpty(saveData.username))
        {
            _connectionUI.SetActive(true);
        }
        else
        {
            var saveDataLocal = await _storageService.LoadAsync<SaveData>(_saveDataName);
            if (!saveData.Equals(saveDataLocal) && (saveData.id == saveDataLocal.id))
            {
                saveData = saveDataLocal;
            }
            else
            {
                await _storageService.SaveAsync(_saveDataName, saveData);
            }

            OnUserDataLoaded?.Invoke(saveData);
            _mainUI.SetActive(true);
            string achievementId = "CgkIyvTP6NIPEAIQAg";
            _achievements.UpdateAchivement(achievementId);
            await SaveGame();
        }
        _adsInitializer.InitializeAds();
    }

    private async void LoadingUserDataFromLocalStorage()
    {
        var saveData = await _storageService.LoadAsync<SaveData>(_saveDataName);
        if (saveData.Equals(null) || string.IsNullOrEmpty(saveData.username))
        {
            _userId = Guid.NewGuid().ToString();
            _connectionUI.SetActive(true);
        }
        else
        {
            OnUserDataLoaded?.Invoke(saveData);
            _mainUI.SetActive(true);
        }
        _adsInitializer.InitializeAds();
    }

    private async void PromptForUsername()
    {
        string username = _usernameInputField.text;
        if (string.IsNullOrEmpty(username))
        {
            return;
        }

        int credits = 0;
        int topScore = 0;
        int gunIndex = 0;

        SaveData currentSaveData = new SaveData
        {
            id = _userId,
            username = username,
            credits = credits,
            topScore = topScore,
            avatarIndex = _avatars.CurrentIndexAvatar,
            gunIndex = new List<int> { gunIndex }
        };

        await _storageService.SaveAsync(_saveDataName, currentSaveData);
        OnUserDataLoaded?.Invoke(currentSaveData);

        DisableUI();

        if (_onLoginToGoogleGames)
        {
            await _userDataStorage.SaveGame(currentSaveData);
        }
    }

    private async void DisableUI()
    {
        await Task.Delay(_timeToDisable);
        _connectionUI.SetActive(false);
        _mainUI.SetActive(true);
    }
}
