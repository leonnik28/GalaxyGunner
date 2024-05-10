using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

public class UnityAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private Credits _credits;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    private Achievements _achievements;

    [SerializeField] private GameSession _gameSession;
    private bool _isAdOnMenu;

    private readonly string _androidAdUnitId = "Rewarded_Android";
    private readonly int _countCreditsForShow = 15;

    [Inject]
    public void Construct(Credits credits, Achievements achievements)
    {
        _credits = credits;
        _achievements = achievements;
    }

    public void LoadAd()
    {
        Advertisement.Load(_androidAdUnitId, this);
    }

    public void ShowAdOnMenu()
    {
        _isAdOnMenu = true;
        Advertisement.Show(_androidAdUnitId, this);
    }

    public async void ShowAdOnGame()
    {
        _isAdOnMenu = false;

        if (_gameSession.OnLoginToGoogleGames)
        {
            await _gameSession.SaveGame();
        }

        Advertisement.Show(_androidAdUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_androidAdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            if (_isAdOnMenu)
            {
                _credits.ChangeCredits(_countCreditsForShow, true);
            }
            else
            {
                _updateGameLevel.Relive();

                if (_gameSession.OnLoginToGoogleGames)
                {
                    string achievementId = "CgkIyvTP6NIPEAIQAw";
                    _achievements.UpdateAchivement(achievementId);
                }
            }
            LoadAd();
        }
    }

    public void OnUnityAdsAdLoaded(string adUnitId) { }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { }
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
