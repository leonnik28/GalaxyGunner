using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class LargeSphere : MonoBehaviour, ILargeSphere
{
    private readonly string _achievementId = "CgkIyvTP6NIPEAIQAQ";

    public void KillLargeSphere()
    {
        IncrementAchievement(_achievementId, 1);
    }

    private void IncrementAchievement(string achievementId, int stepsToIncrement)
    {
        bool isUnlocked = false;
        Social.localUser.Authenticate(success =>
        {
            if (success == true)
            {
                CheckToUnlockedAchevement(achievementId, isUnlocked);
                if (!isUnlocked)
                {
                    PlayGamesPlatform.Instance.IncrementAchievement(achievementId, stepsToIncrement, success => { });
                }
            }
        });
    }

    private void CheckToUnlockedAchevement(string achievementId, bool isUnlocked)
    {
        PlayGamesPlatform.Instance.LoadAchievements((achievements) =>
        {
            foreach (IAchievement achievement in achievements)
            {
                if (achievement.id == achievementId)
                {
                    isUnlocked = achievement.completed;
                    break;
                }
            }
        });
    }
}
