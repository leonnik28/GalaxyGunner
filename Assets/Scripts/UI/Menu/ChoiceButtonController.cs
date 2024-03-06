using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _profileUI;
    [SerializeField] private GameObject _achievementsUI;
    [SerializeField] private GameObject _leaderboardUI;

    public void ChoiceProfileUI()
    {
        _profileUI.SetActive(true);
        _achievementsUI.SetActive(false);
        _leaderboardUI.SetActive(false);
    }

    public void ChoiceAchievementsUI()
    {
        _profileUI.SetActive(false);
        _achievementsUI.SetActive(true);
        _leaderboardUI.SetActive(false);
    }

    public void ChoiceLeaderboardUI()
    {
        _profileUI.SetActive(false);
        _achievementsUI.SetActive(false);
        _leaderboardUI.SetActive(true);
    }
}
