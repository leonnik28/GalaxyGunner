using System;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Action OnExitClicked;
    public Action<string> OnGunBuy;

    [SerializeField] private GameSession _gameSession;
    [SerializeField] private Achievements _achievements;

    [SerializeField] private GameUIController _gameUIController;
    [SerializeField] private GameObject _purchaseUI;
    [SerializeField] private GameObject _fieldPurchaseUI;

    [SerializeField] private TextMeshProUGUI _costText;

    private GameObject _gunToPurchase;

    private Credits _credits;
    private GunInventory _gunInventory;
    private GameObject _currentGunObject;

    private void Awake()
    {
        _credits = _gameSession.GetComponent<Credits>();
        _gunInventory = _gameSession.GetComponent<GunInventory>();

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
        await _gameSession.SaveGame();
        _purchaseUI.SetActive(false);
        _gameUIController.DisableGameUI();
        OnGunBuy?.Invoke(_gunInventory.GunInShop.Name);
        if(_gunInventory.GunInShop.Index == 2)
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
