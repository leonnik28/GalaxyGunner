using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UserDataStorage;

public class TopScore : MonoBehaviour
{
    public int CurrentTopScore => _topScore;

    [SerializeField] private GameSession _gameSession;
    [SerializeField] private Score score;
    [SerializeField] private TextMeshProUGUI _topScoreText;

    private int _topScore;

    private void Start()
    {
        _gameSession.OnUserDataLoaded += LoadTopScore;
    }

    private void LoadTopScore(SaveData saveData)
    {
        _topScore = saveData.topScore;
        _topScoreText.text = _topScore.ToString();
    }
}
