using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UserDataStorage;

public class GunInventory : MonoBehaviour
{
    public Gun Gun => _gun;
    public GameObject GunObject => _currentGunObject;

    public event Action<Gun> OnGunChanged;

    [SerializeField] private List<GameObject> _gunUIList;

    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GunPool _gunPool;

    [SerializeField] private GameObject _shopTab;
    [SerializeField] private GameObject _inventoryTab;

    private List<Gun> _gunShopList;
    private List<Gun> _gunInventoryList;
    private Gun _gun;
    private GameObject _currentGunObject;

    private Credits _credits;

    private void Start()
    {
        _credits = GetComponent<Credits>();

        _gameSession.OnUserDataLoaded += LoadGuns;

        _gunShopList = new List<Gun>();
        _gunInventoryList = new List<Gun>();
    }

    private void LoadGuns(SaveData saveData)
    {
        ClearGuns();
        foreach (var gunIndex in saveData.gunIndex)
        {
            AddGunToInventory(gunIndex);
        }

        AddGunToShop();
    }

    private void ClearGuns()
    {
        _gunInventoryList.Clear();
        _gunShopList.Clear();
        ClearChildren(_inventoryTab.transform);
        ClearChildren(_shopTab.transform);
    }

    private void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddGunToInventory(int gunIndex)
    {
        Gun gun = _gunPool.GetGun(gunIndex);
        if (gun != null)
        {
            InstantiateGun(gunIndex, _inventoryTab, _gunInventoryList, gun);
        }
    }

    private void AddGunToShop()
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
        buttonGun.onClick.AddListener(() => ChooseGun(gun, gunObject));
        gunList.Add(gun);
    }

    private void ChooseGun(Gun gun, GameObject gunObject)
    {
        _gun = gun;
        _currentGunObject = gunObject;
        OnGunChanged?.Invoke(_gun);
    }
}
