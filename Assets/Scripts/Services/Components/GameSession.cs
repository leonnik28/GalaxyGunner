using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UserDataStorage;

public class GameSession : MonoBehaviour
{
    public event Action<SaveData> OnUserDataLoaded;
    public event Action OnSuccessLogin;

    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _connectionUI;
    [SerializeField] private GameObject _errorUI;

    [SerializeField] private Achievements _achievements;
    [SerializeField] private StorageService _storageService;

    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private Button _submitButton;

    private UserDataStorage _userDataStorage;
    private Avatars _avatars;
    private string _userId;
    private string _saveDataName = "saveData";

    private void Awake()
    {
        _storageService = new StorageService();

        _userDataStorage = gameObject.GetComponent<UserDataStorage>();
        _avatars = gameObject.GetComponent<Avatars>();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        _submitButton.onClick.AddListener(PromptForUsername);
    }

    private async void Start()
    {
        var tcs = new TaskCompletionSource<bool>();
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            if (success == SignInStatus.Success)
            {
                _userId = PlayGamesPlatform.Instance.localUser.id;
                tcs.SetResult(true);
                OnSuccessLogin?.Invoke();
            }
            else
            {
                _errorUI.SetActive(true);
                tcs.SetResult(false);
            }
        });

        if (!await tcs.Task)
        {
            return;
        }

        var saveData = await _userDataStorage.LoadGame(_userId);
        if (saveData.Equals(null) || string.IsNullOrEmpty(saveData.username))
        {
            _connectionUI.SetActive(true);
        }
        else
        {
            var saveDataLocal = await _storageService.LoadAsync<SaveData>(_saveDataName);
            if(!saveData.Equals(saveDataLocal))
            {
                saveData = saveDataLocal;
            }
            OnUserDataLoaded?.Invoke(saveData);
            _mainUI.SetActive(true);
            string achievementId = "CgkIyvTP6NIPEAIQAg";
            _achievements.UpdateAchivement(achievementId);
            await SaveGame();
        }
    }

    private async void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            await SaveGame();
        }
    }

    private async void OnApplicationQuit()
    {
        await SaveGame();
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

        await _userDataStorage.SaveGame(saveData);
    }

    public async Task SaveGame()
    {
        var saveData = await _storageService.LoadAsync<SaveData>(_saveDataName);
        await _userDataStorage.SaveGame(saveData);
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
        await _userDataStorage.SaveGame(currentSaveData);
    }

    private async void DisableUI()
    {
        await Task.Delay(1000);
        _connectionUI.SetActive(false);
        _mainUI.SetActive(true);
    }
}
