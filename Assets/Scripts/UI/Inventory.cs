using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UserDataStorage;
using UnityEngine.Profiling;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Gun Gun => _gun;

    public event Action<Gun> OnGunChanged;

    [SerializeField] private List<GameObject> _gunUIList;
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private GameObject _gunTab;
    [SerializeField] private GunPool _gunPool;

    private List<Gun> _gunList;
    private Gun _gun;

    private void Start()
    {
        _gameSession.OnUserDataLoaded += HandleUserDataLoaded;
        _gunList = new List<Gun>();
    }

    public void ChooseGun(int index)
    {
        _gun = _gunList[index];
        OnGunChanged?.Invoke(_gun);
    }

    private void HandleUserDataLoaded(SaveData saveData)
    {
        foreach (var gunIndex in saveData.gunIndex)
        {
            Gun gun = _gunPool.GetGun(gunIndex);
            if (gun != null)
            {
                GameObject gunObject = Instantiate(_gunUIList[gunIndex], _gunTab.transform);
                Button buttonGun = gunObject.GetComponent<Button>();
                buttonGun.onClick.AddListener(() => ChooseGun(gunIndex));
                _gunList.Add(gun);
            }
        }
    }

    private Gun FindGun(int index)
    {
        return _gunList.FirstOrDefault(gun => gun.Index == index);
    }
}
