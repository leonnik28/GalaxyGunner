using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public void UpdateAchivement(string achievementId)
    {
        bool isUnlocked = false;
        CheckToUnlockedAchevement(achievementId, isUnlocked);
        if (!isUnlocked)
        {
            Social.ReportProgress(achievementId, 100.0, success => { });
        }
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
