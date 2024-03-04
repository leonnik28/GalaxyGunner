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

    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _connectionUI;
    [SerializeField] private GameObject _errorUI;

    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private Button _submitButton;

    private UserDataStorage _userDataStorage;
    private string _userId;

    private void Awake()
    {
        _userDataStorage = gameObject.GetComponent<UserDataStorage>();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        _submitButton.onClick.AddListener(PromptForUsername);
        _submitButton.onClick.AddListener(DisableUI);
    }

    private async void Start()
    {
        _userId = PlayGamesPlatform.Instance.localUser.id;
        /*var tcs = new TaskCompletionSource<bool>();
        PlayGamesPlatform.Instance.Authenticate(success =>
        {
            if (success == SignInStatus.Success)
            {
                _userId = PlayGamesPlatform.Instance.localUser.id;
                tcs.SetResult(true);
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
        }*/

        var saveData = await _userDataStorage.LoadGame(_userId);

        if (string.IsNullOrEmpty(saveData.username))
        {
            _connectionUI.SetActive(true);
        }
        else
        {
            OnUserDataLoaded?.Invoke(saveData);
            _mainUI.SetActive(true);
        }
    }

    public void LoadProfileImage(string imageUrl, Action<Sprite> callback)
    {
        StartCoroutine(_userDataStorage.LoadProfileImage(imageUrl, texture =>
        {
            if (texture != null)
            {
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callback(sprite);
            }
            else
            {
                callback(null);
            }
        }));
    }

    public async void SaveGame(int credits = 0, string image = null, int gunIndex = 0)
    {
        var saveData = await _userDataStorage.LoadGame(_userId);
        if (credits != 0)
        {
            saveData.credits = credits;
        }
        if (image != null)
        {
            saveData.profileImage = image;
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
            Debug.LogError("Username is required");
            return;
        }

        int credits = 0;
        int gunIndex = 0;

        SaveData currentSaveData = new SaveData
        {
            id = _userId,
            username = username,
            credits = credits,
            gunIndex = new List<int> { gunIndex }
        };

        await _userDataStorage.SaveGame(currentSaveData);
        OnUserDataLoaded?.Invoke(currentSaveData);
    }

    private async void DisableUI()
    {
        await Task.Delay(1000);
        _connectionUI.SetActive(false);
        _mainUI.SetActive(true);
    }
}