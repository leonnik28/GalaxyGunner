using UnityEngine;
using static UserDataStorage;

public class Credits : MonoBehaviour
{
    public int CurrentCredits => _credits;

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
    }

    private void LoadCredits(SaveData saveData)
    {
        _credits = saveData.credits;
    }
}
