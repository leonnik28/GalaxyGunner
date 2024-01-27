using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateChunk : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject1;
    [SerializeField] private GameObject _gameObject2;
    [SerializeField] private GameObject _gameObject3;
    [SerializeField] private GameObject _gameObject4;

    private void Start()
    {
        if(_gameObject1 != null)
        {
            _gameObject1.SetActive(true);
        }
        if(_gameObject2 != null)
        {
            _gameObject2.SetActive(true);
        }
        if(_gameObject3 != null)
        {
            _gameObject3.SetActive(true);
        }
        if (_gameObject4 != null)
        {
            _gameObject4.SetActive(true);
        }
    }
}
