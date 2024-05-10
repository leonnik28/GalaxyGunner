using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static UserDataStorage;

public class ProfileData : IInitializable, IDisposable
{
    public string Username => _username;
    public string Id => _id;
    public string Credits => _credits;
    public string TopScore => _topScore;
    public Image ProfileImage => _profileImage;

    public Action OnProfileDataChanged;

    private Avatars _avatars;
    private GameSession _gameSession;

    private string _username;
    private string _id;
    private string _credits;
    private string _topScore;
    private Image _profileImage;

    private ProfileData(GameSession gameSession, Avatars avatars)
    {
        _gameSession = gameSession;
        _avatars = avatars;
    }

    public void Initialize()
    {
        _gameSession.OnUserDataLoaded += HandleUserDataLoaded;
        _avatars.AvatarChanged += ChangeAvatar;
    }

    public void Dispose()
    {
        _gameSession.OnUserDataLoaded -= HandleUserDataLoaded;
        _avatars.AvatarChanged -= ChangeAvatar;
    }

    private void HandleUserDataLoaded(SaveData saveData)
    {
        _username = saveData.username;
        _id = saveData.id;
        _profileImage = _avatars.GetAvatar(saveData.avatarIndex);
        _credits = saveData.credits.ToString();
        _topScore = saveData.topScore.ToString();

        OnProfileDataChanged?.Invoke();
    }

    private void ChangeAvatar()
    {
        _profileImage = _avatars.GetAvatar(_avatars.CurrentIndexAvatar);
        OnProfileDataChanged?.Invoke();
    }
}
