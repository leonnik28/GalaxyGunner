using UnityEngine;

public class ChoiceButtonController : MonoBehaviour
{
    [SerializeField] private GameSession _gameSession;

    public void ChoiceAchievementsUI()
    {
        if (_gameSession.OnLoginToGoogleGames)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void ChoiceLeaderboardUI()
    {
        if (_gameSession.OnLoginToGoogleGames)
        {
            Social.ShowLeaderboardUI();
        }
    }
}
