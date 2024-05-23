using System;
using Zenject;
using static UserDataStorage;

public class Credits : IInitializable, IDisposable
{
    public int CurrentCredits => _credits;

    public event Action <int, string> OnChangeCredits;

    private GameSession _gameSession;
    private int _credits;

    private Credits(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    public void Initialize()
    {
        _gameSession.OnUserDataLoaded += LoadCredits;
    }

    public void Dispose()
    {
        _gameSession.OnUserDataLoaded -= LoadCredits;
    }

    public async void ChangeCredits(int amount, bool save = false)
    {
        if (amount != 0)
        {
            _credits += amount;
            if (save)
            {
                await _gameSession.SaveGame(_credits);
            }

            if (amount > 0)
            {
                OnChangeCredits?.Invoke(amount, "+");
            }
            else
            {
                OnChangeCredits?.Invoke(amount, "");
            }

            if (_gameSession.OnLoginToGoogleGames)
            {
                await _gameSession.SaveGame();
            }
        }
    }

    private void LoadCredits(SaveData saveData)
    {
        _credits = saveData.credits;
    }
}
