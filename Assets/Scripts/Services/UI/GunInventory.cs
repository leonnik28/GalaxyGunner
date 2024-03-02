using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UserDataStorage;
using UnityEngine.Profiling;
using System.Linq;
using UnityEngine.UI;

public class GunInventory : MonoBehaviour
{
    public Gun Gun => _gun;

    public event Action<Gun> OnGunChanged;

    [SerializeField] private List<GameObject> _gunUIList;
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GameObject _shopTab;
    [SerializeField] private GameObject _inventoryTab;
    [SerializeField] private GunPool _gunPool;

    private List<Gun> _gunShopList;
    private List<Gun> _gunInventoryList;
    private Gun _gun;

    private void Start()
    {
        _gameSession.OnUserDataLoaded += LoadGuns;

        _gunShopList = new List<Gun>();
        _gunInventoryList = new List<Gun>();
    }

    private void LoadGuns(SaveData saveData)
    {
        foreach (var gunIndex in saveData.gunIndex)
        {
            AddGunToInventory(gunIndex);
        }

        AddGunToShop();
    }

    private void AddGunToInventory(int gunIndex)
    {
        Gun gun = _gunPool.GetGun(gunIndex);
        if (gun != null)
        {
            InstantiateGun(gunIndex, _inventoryTab, _gunInventoryList, gun);
        }
    }

    private  void AddGunToShop()
    {
        for (int index = 0; index < _gunPool.GetCountGuns(); index++)
        {
            Gun gun = _gunPool.GetGun(index);

            bool isGunInInventory = _gunInventoryList.Any(g => g == gun);

            if (!isGunInInventory)
            {
                InstantiateGun(index, _shopTab, _gunShopList, gun);
            }
        }
    }

    private void InstantiateGun(int gunIndex, GameObject tab, List<Gun> gunList, Gun gun)
    {
        GameObject gunObject = Instantiate(_gunUIList[gunIndex], tab.transform);
        Button buttonGun = gunObject.GetComponent<Button>();
        buttonGun.onClick.AddListener(() => ChooseGun(gun));
        gunList.Add(gun);
    }

    private void ChooseGun(Gun gun)
    {
        _gun = gun;
        OnGunChanged?.Invoke(_gun);
    }
}
