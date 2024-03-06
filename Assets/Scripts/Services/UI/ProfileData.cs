using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UserDataStorage;

public class ProfileData : MonoBehaviour
{
    public string Username => _username;
    public string Id => _id;
    public string Credits => _credits;
    public Image ProfileImage => _profileImage;

    public Action OnProfileDataChanged;

    private Avatars _avatars;
    private GameSession _gameSession;

    private string _username;
    private string _id;
    private string _credits;
    private Image _profileImage;

    private void Awake()
    {
        _gameSession = GetComponent<GameSession>();
        _avatars = GetComponent<Avatars>();
        _gameSession.OnUserDataLoaded += HandleUserDataLoaded;
    }

    private void HandleUserDataLoaded(SaveData saveData)
    {
        _username = saveData.username;
        _id = saveData.id;
        _profileImage = _avatars.GetAvatar(saveData.avatarIndex);
        _credits = saveData.credits.ToString();

        OnProfileDataChanged?.Invoke();
    }
}
