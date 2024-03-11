using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UserDataStorage;

public class TopScore : MonoBehaviour
{
    public int CurrentTopScore => _topScore;

    public event Action OnTopScoreChange;

    [SerializeField] private GameSession _gameSession;
    [SerializeField] private Score score;

    private int _topScore;
    private string _leaderboardId = "CgkIyvTP6NIPEAIQBQ";

    private void Start()
    {
        _gameSession.OnUserDataLoaded += LoadTopScore;
        OnTopScoreChange += UpdateLeaderboard;
    }

    private void LoadTopScore(SaveData saveData)
    {
        _topScore = saveData.topScore;
    }

    private void UpdateLeaderboard()
    {
        if (_topScore >= 10000)
        {
            Social.ReportScore(_topScore, _leaderboardId, success => { });
        }
    }
}
