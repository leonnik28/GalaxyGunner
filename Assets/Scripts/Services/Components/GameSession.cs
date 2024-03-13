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

    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private Button _submitButton;

    private UserDataStorage _userDataStorage;
    private Avatars _avatars;
    private string _userId;

    private void Awake()
    {
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
        if (string.IsNullOrEmpty(saveData.username))
        {
            _connectionUI.SetActive(true);
        }
        else
        {
            OnUserDataLoaded?.Invoke(saveData);
            _mainUI.SetActive(true);
            string achievementId = "CgkIyvTP6NIPEAIQAg";
            _achievements.UpdateAchivement(achievementId);
        }
    }

    public async void SignInGoogle()
    {
        var tcs = new TaskCompletionSource<bool>();
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            if (success == SignInStatus.Success)
            {
                _userId = PlayGamesPlatform.Instance.localUser.id;
                tcs.SetResult(true);
            }
            else
            {
                _mainUI.SetActive(false);
                _errorUI.SetActive(true);
                tcs.SetResult(false);
            }
        });

        var saveData = await _userDataStorage.LoadGame(_userId);

        if (string.IsNullOrEmpty(saveData.username))
        {
            _mainUI.SetActive(false);
            _connectionUI.SetActive(true);
        }
        else
        {
            OnUserDataLoaded?.Invoke(saveData);
        }
    }

    public async void SaveGame(int credits = 0, int topScore = 0, int avatarIndex = -1, int gunIndex = 0)
    {
        var saveData = await _userDataStorage.LoadGame(_userId);
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

        await _userDataStorage.SaveGame(saveData);
        OnUserDataLoaded?.Invoke(saveData);
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

        await _userDataStorage.SaveGame(currentSaveData);
        OnUserDataLoaded?.Invoke(currentSaveData);

        DisableUI();
    }

    private async void DisableUI()
    {
        await Task.Delay(1000);
        _connectionUI.SetActive(false);
        _mainUI.SetActive(true);
    }
}
