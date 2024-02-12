using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Vector3 _score;
    [SerializeField] private Transform _player;

    private event Action<Vector3> OnPlayerMoved;

    private Vector3 _startPosition;
    private Vector3 _lastPosition;

    private void Start()
    {
        _startPosition = _player.position;
        _lastPosition = _player.position;
    }

    private void FixedUpdate()
    {
        if (_player.position != _lastPosition)
        {
            OnPlayerMoved?.Invoke(_player.position);
            _lastPosition = _player.position;
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

    private void UpdateScore(Vector3 newPosition)
    {
        _score = _player.position - _startPosition;
    }
}
