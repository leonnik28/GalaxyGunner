using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UserDataStorage;

public class Avatars : MonoBehaviour
{
    public int CurrentIndexAvatar => _currentIndexAvatar;

    [SerializeField] private List<GameObject> _avatars;
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GameObject _avatarGroup;
    [SerializeField] private GameObject _avatarsUI;

    private int _currentIndexAvatar;

    private void Start()
    {
        foreach (var avatar in _avatars)
        {
            GameObject avatarObject = Instantiate(avatar, _avatarGroup.transform);
            Button avatarButton = avatarObject.GetComponent<Button>();
            avatarButton.onClick.AddListener(() => ChangeAvatar(_avatars.IndexOf(avatar)));
        }
    }

    public void OpenAvatarsUI()
    {
        _avatarsUI.SetActive(true);
    }

    public void ChangeAvatar(int index)
    {
        _currentIndexAvatar = index;
        _gameSession.SaveGame(avatarIndex:  _currentIndexAvatar);
        _avatarsUI.SetActive(false);
    }

    public Image GetAvatar(int index)
    {
        for (int i = 0;  i < _avatars.Count; i++)
        {
            if(i == index)
            {
                return _avatars[i].GetComponent<Image>();
            }
        }

        return null;
    }

    private void LoadIndex(SaveData saveData)
    {
        _currentIndexAvatar = saveData.avatarIndex;
    }
}
