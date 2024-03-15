using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int CurrentScore => (int)_score;

    [SerializeField] private Transform _model;
    [SerializeField] private TextMeshProUGUI _textScore;

    private event Action<float> OnPlayerMoved;

    private float _score;
    private float _startPosition;
    private float _lastPosition;

    private readonly int _scoreFactor = 1;

    private void Start()
    {
        _startPosition = _model.position.z;
        _lastPosition = _model.position.z;
    }

    private void FixedUpdate()
    {
        if (_model.position.z != _lastPosition)
        {
            OnPlayerMoved?.Invoke(_model.position.z);
            _lastPosition = _model.position.z;
        }
    }

    private void OnEnable()
    {
        OnPlayerMoved += UpdateScore;
    }

    private void OnDisable()
    {
        OnPlayerMoved -= UpdateScore;
    }

    private void UpdateScore(float newPosition)
    {
        _score = (int)(_model.position.z - _startPosition) / _scoreFactor;
        _textScore.text = "Score: " + _score;
    }
}
