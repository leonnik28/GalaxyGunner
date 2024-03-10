using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexAds : MonoBehaviour
{
    [SerializeField] private Credits _credits;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private Achievements _achievements;

    private RewardedAdLoader _rewardedAdLoader;
    private RewardedAd _rewardedAd;
    private string _adUnitId = "R-M-6526269-1";

    private int _countCreditsForShow = 5;

    private void Awake()
    {
        SetupLoader();
        RequestRewardedAd();
    }

    public void ShowRewardedAdOnMenu()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.OnRewarded += HandleRewardedOnMenu;
            _rewardedAd.Show();
        }
    }

    public void ShowRewardedAdOnGame()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.OnRewarded += HandleRewardedOnGame;
            _rewardedAd.Show();
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

        string achievementId = "CgkIyvTP6NIPEAIQAw";
        _achievements.UpdateAchivement(achievementId);

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
