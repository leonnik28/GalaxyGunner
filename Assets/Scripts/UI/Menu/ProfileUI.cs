using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    [SerializeField] private ProfileData _profileData;

    [SerializeField] private TextMeshProUGUI _username;
    [SerializeField] private TextMeshProUGUI _id;
    [SerializeField] private TextMeshProUGUI _credits;
    [SerializeField] private Image _profileImage;

    private void Awake()
    {
        _profileData.OnProfileDataChanged += ChangeProfile;
    }

    private void OnEnable()
    {
        _username.text = _profileData.Username;
        if (_id != null)
        {
            _id.text = "Id: " + _profileData.Id;
        }
        if (_credits != null)
        {
            _credits.text = "Credits: " + _profileData.Credits;
        }
        _profileImage.sprite = _profileData.ProfileImage.sprite;
    }

    private void ChangeProfile()
    {
        if (_credits != null)
        {
            _credits.text = "Credits: " + _profileData.Credits;
        }
        _profileImage.sprite = _profileData.ProfileImage.sprite;
    }
}
