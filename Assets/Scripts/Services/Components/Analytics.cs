using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Zenject;

public class Analytics : IInitializable
{
    async public void Initialize()
    {
        await UnityServices.InitializeAsync();
        GiveConsent();
    }

    public void OnPlayerDeath(int biomId, int currentScore)
    {
        var data = new Dictionary<string, object>
        {
            { "biomId", biomId },
            { "currentScore", currentScore }
        };

        AnalyticsService.Instance.CustomData("playerDeath", data);
    }

    public void OnPlayerStartGame(string gunName)
    {
        var data = new Dictionary<string, object>
        {
            { "gunName", gunName }
        };

        AnalyticsService.Instance.CustomData("playerStart", data);
    }

    private void GiveConsent()
    {
        AnalyticsService.Instance.StartDataCollection();
    }
}
