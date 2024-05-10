using System;
using UnityEngine;
using Zenject;
using static UserDataStorage;

public class TopScore : IInitializable, IDisposable
{
    public int CurrentTopScore => _topScore;

    public event Action OnTopScoreChange;

    private GameSession _gameSession;

    private int _topScore;

    private readonly string _leaderboardId = "CgkIyvTP6NIPEAIQBQ";
    private readonly int _minimalScoreLeaderboard = 10000;

    private TopScore(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    public void Initialize()
    {
        _gameSession.OnUserDataLoaded += LoadTopScore;
        OnTopScoreChange += UpdateLeaderboard;
    }

    public void Dispose()
    {
        _gameSession.OnUserDataLoaded -= LoadTopScore;
        OnTopScoreChange -= UpdateLeaderboard;
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
