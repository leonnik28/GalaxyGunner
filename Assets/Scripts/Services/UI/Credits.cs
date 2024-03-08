using System;
using UnityEngine;
using static UserDataStorage;

public class Credits : MonoBehaviour
{
    public int CurrentCredits => _credits;

    public event Action <int, string> OnChangeCredits;

    [SerializeField] private GameSession _gameSession;

    private int _credits;

    private void Start()
    {
        _gameSession.OnUserDataLoaded += LoadCredits;
    }

    public void ChangeCredits(int amount, bool save = false)
    {
        _credits += amount;
        if (save)
        {
            _gameSession.SaveGame(_credits);
        }

        if (amount > 0)
        {
            OnChangeCredits?.Invoke(amount, "+");
        }
        else
        {
            OnChangeCredits?.Invoke(amount, "");
        }
    }

    private void LoadCredits(SaveData saveData)
    {
        _credits = saveData.credits;
    }
}
