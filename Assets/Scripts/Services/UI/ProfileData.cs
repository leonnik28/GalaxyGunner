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

    private GameSession _gameSession;

    private string _username;
    private string _id;
    private string _credits;
    private Image _profileImage;

    private void Awake()
    {
        _gameSession = GetComponent<GameSession>();
        _gameSession.OnUserDataLoaded += HandleUserDataLoaded;
    }

    private void HandleUserDataLoaded(SaveData saveData)
    {
        _username = saveData.username;
        _id = saveData.id;
        _credits = saveData.credits.ToString();

        if (saveData.profileImage != "")
        {
            /*_gameSession.LoadProfileImage(saveData.profileImage, sprite =>
            {
                if (sprite != null)
                {
                    _profileImage.sprite = sprite;
                }
            });*/
        }
    }
}
