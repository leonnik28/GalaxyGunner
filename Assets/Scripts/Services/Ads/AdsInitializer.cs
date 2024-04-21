using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private bool _testMode = true;

    public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_androidGameId, _testMode, this);
        }
    }

    public void OnInitializationComplete() { }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }
}