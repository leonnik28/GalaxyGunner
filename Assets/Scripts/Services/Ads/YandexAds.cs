using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexAds : MonoBehaviour
{
    [SerializeField] private Credits _credits;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private Achievements _achievements;

    private RewardedAdLoader _rewardedAdLoader;
    private RewardedAd _rewardedAd;

    private GameSession _gameSession;

    private readonly string _adUnitId = "R-M-6526269-1";
    private readonly int _countCreditsForShow = 5;

    private void Awake()
    {
        SetupLoader();
        RequestRewardedAd();

        _gameSession = _credits.gameObject.GetComponent<GameSession>();
    }

    public void ShowRewardedAdOnMenu()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.OnRewarded += HandleRewardedOnMenu;
            _rewardedAd.Show();
        }
    }

    public async void ShowRewardedAdOnGame()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.OnRewarded += HandleRewardedOnGame;
            _rewardedAd.Show();

            if (_gameSession.OnLoginToGoogleGames)
            {
                await _gameSession.SaveGame();
            }
        }
    }

    private void SetupLoader()
    {
        _rewardedAdLoader = new RewardedAdLoader();
        _rewardedAdLoader.OnAdLoaded += HandleAdLoaded;
    }

    private void RequestRewardedAd()
    {
        AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(_adUnitId).Build();
        _rewardedAdLoader.LoadAd(adRequestConfiguration);
    }

    private void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        _rewardedAd = args.RewardedAd;

        _rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
        _rewardedAd.OnAdDismissed += HandleAdDismissed;
    }

    private void HandleAdDismissed(object sender, EventArgs args)
    {
        DestroyRewardedAd();
        RequestRewardedAd();
    }

    private void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    { 
        DestroyRewardedAd();
        RequestRewardedAd();
    }

    private void HandleRewardedOnMenu(object sender, Reward args)
    {
        _credits.ChangeCredits(_countCreditsForShow, true);
        _rewardedAd.OnRewarded -= HandleRewardedOnMenu;
    }

    private void HandleRewardedOnGame(object sender, Reward args)
    {
        _updateGameLevel.Relive();

        if (_gameSession.OnLoginToGoogleGames)
        {
            string achievementId = "CgkIyvTP6NIPEAIQAw";
            _achievements.UpdateAchivement(achievementId);
        }

        _rewardedAd.OnRewarded -= HandleRewardedOnGame;
    }

    private void DestroyRewardedAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
    }
}
