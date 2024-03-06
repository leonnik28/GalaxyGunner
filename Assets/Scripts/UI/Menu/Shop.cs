using System;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Action OnExitClicked;

    [SerializeField] private GameSession _gameSession;

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
        Gun selectedGun = _gunInventory.Gun;
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

    public void ConfirmBuy()
    {
        _credits.ChangeCredits(-_gunInventory.Gun.Cost);
        _gameSession.SaveGame(credits: _credits.CurrentCredits, gunIndex: _gunInventory.Gun.Index);
        _purchaseUI.SetActive(false);
        _gameUIController.DisableGameUI();
        OnExitClicked?.Invoke();
    }

    private void ChangeCurrentGun()
    {
        Destroy(_currentGunObject);
    }
}
