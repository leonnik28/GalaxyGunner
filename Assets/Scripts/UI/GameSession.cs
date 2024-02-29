using GooglePlayGames;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UserDataStorage;

public class GameSession : MonoBehaviour
{
    public event Action<SaveData> OnUserDataLoaded;

    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _currentUI;

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
        var saveData = await _userDataStorage.LoadGame(_userId);

        if (string.IsNullOrEmpty(saveData.username))
        {
            _currentUI.SetActive(true);
        }
        else
        {
            _usernameInputField.text = saveData.username;
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

    private async void PromptForUsername()
    {
        SaveData currentSaveData = new SaveData();

        string username = _usernameInputField.text;
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username is required");
            return;
        }

        int credits = 0;
        string gunName = "Pistol";

        currentSaveData.id = _userId;
        currentSaveData.username = username;
        currentSaveData.credits = credits;
        //currentSaveData.gunNames.Add(gunName);

        await _userDataStorage.SaveGame(currentSaveData);
        OnUserDataLoaded?.Invoke(currentSaveData);
    }

    private async void DisableUI()
    {
        await Task.Delay(1000);
        _gameUI.SetActive(false);
        _mainUI.SetActive(true);
    }
}