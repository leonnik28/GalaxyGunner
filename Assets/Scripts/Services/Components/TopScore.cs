using System;
using UnityEngine;
using static UserDataStorage;

public class TopScore : MonoBehaviour
{
    public int CurrentTopScore => _topScore;

    public event Action OnTopScoreChange;

    [SerializeField] private GameSession _gameSession;
    [SerializeField] private Score score;

    private int _topScore;

    private readonly string _leaderboardId = "CgkIyvTP6NIPEAIQBQ";
    private readonly int _minimalScoreLeaderboard = 10000;

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
        if (_topScore >= _minimalScoreLeaderboard)
        {
            Social.ReportScore(_topScore, _leaderboardId, success => { });
        }
    }
}
