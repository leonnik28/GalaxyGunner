using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeahtReliveUI : MonoBehaviour
{
    [SerializeField] private RoadGenerate _roadGenerate;
    [SerializeField] private Chunk _chunk;
    [SerializeField] private GameObject _deathUI;
    [SerializeField] private GameObject _reliveUI;
    [SerializeField] private int _healthIndex = 1;

    public void GameOver()
    {
        _chunk = Instantiate(_chunk, transform);
        _roadGenerate.ChangeChunk(_chunk);
        Time.timeScale = 0f;

        if (_healthIndex >= 1)
        {
            _healthIndex--;
            _reliveUI.SetActive(true);
        }
        else
        {
            _healthIndex = 1;
            _deathUI.SetActive(true);
        }
    }
}
