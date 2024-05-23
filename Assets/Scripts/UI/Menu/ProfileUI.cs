using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProfileUI : MonoBehaviour
{
    private ProfileData _profileData;

    [SerializeField] private TextMeshProUGUI _username;
    [SerializeField] private TextMeshProUGUI _id;
    [SerializeField] private TextMeshProUGUI _credits;
    [SerializeField] private TextMeshProUGUI _topScore;
    [SerializeField] private Image _profileImage;

    [Inject]
    private void Construct(ProfileData profileData)
    {
        _profileData = profileData;
    }

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
        if (_topScore != null)
        {
            _topScore.text = "Top score: " + _profileData.TopScore;
        }
        _profileImage.sprite = _profileData.ProfileImage.sprite;
    }

    private void ChangeProfile()
    {
        if (_credits != null)
        {
            _credits.text = "Credits: " + _profileData.Credits;
        }
        if (_topScore != null)
        {
            _topScore.text = "Top score: " + _profileData.TopScore;
        }
        _profileImage.sprite = _profileData.ProfileImage.sprite;
    }
}
