using System;
using UnityEngine;
using TMPro;
using Zenject;

public class Shop : MonoBehaviour
{
    public Action OnExitClicked;
    public Action<string> OnGunBuy;

    [SerializeField] private GameSession _gameSession;
    private Achievements _achievements;

    [SerializeField] private GameUIController _gameUIController;
    [SerializeField] private GameObject _purchaseUI;
    [SerializeField] private GameObject _fieldPurchaseUI;

    [SerializeField] private TextMeshProUGUI _costText;

    private GameObject _gunToPurchase;

    private Credits _credits;
    [SerializeField] private GunInventory _gunInventory;
    private GameObject _currentGunObject;

    [Inject]
    public void Construct(Credits credits, Achievements achievements)
    {
        _credits = credits;
        _achievements = achievements;
    }

    private void Awake()
    {
        OnExitClicked += ChangeCurrentGun;
    }

    public void BuyGun()
    {
        Gun selectedGun = _gunInventory.GunInShop;
        if (selectedGun != null && selectedGun.Cost <= _credits.CurrentCredits)
        {
            _gunToPurchase = _gunInventory.GunObject;
            _purchaseUI.SetActive(true);
            _costText.text = "Cost: " + selectedGun.Cost.ToString();
            GameObject gunObject = Instantiate(_gunToPurchase, _fieldPurchaseUI.transform);
            _currentGunObject = gunObject;
        }
    }

    public void Exit()
    {
        _purchaseUI.SetActive(false);
        OnExitClicked?.Invoke();
    }

    public async void ConfirmBuy()
    {
        _credits.ChangeCredits(-_gunInventory.GunInShop.Cost);
        await _gameSession.SaveGame(credits: _credits.CurrentCredits, gunIndex: _gunInventory.GunInShop.Index);
        _purchaseUI.SetActive(false);
        _gameUIController.DisableGameUI();
        OnGunBuy?.Invoke(_gunInventory.GunInShop.Name);
        if(_gunInventory.GunInShop.Index == 2 && _gameSession.OnLoginToGoogleGames)
        {
            string achievementId = "CgkIyvTP6NIPEAIQAA";
            _achievements.UpdateAchivement(achievementId);
        }
        OnExitClicked?.Invoke();
    }

    private void ChangeCurrentGun()
    {
        Destroy(_currentGunObject);
    }
}
