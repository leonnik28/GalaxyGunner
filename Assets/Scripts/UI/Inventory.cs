using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UserDataStorage;
using UnityEngine.Profiling;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public Gun Gun => _gun;

    public event Action<Gun> OnGunChanged;

    [SerializeField] private GameSession _gameSession;

    private List<Gun> _gunList;
    private Gun _gun;

    private void Start()
    {
        _gameSession.OnUserDataLoaded += HandleUserDataLoaded;
    }

    public void ChooseGun(int index)
    {
        _gun = _gunList[index];
        OnGunChanged?.Invoke(_gun);
    }

    private void HandleUserDataLoaded(SaveData saveData)
    {
        foreach (var gunData in saveData.gunNames)
        {
            Gun gun = FindGun(gunData);
            if (gun != null)
            {
                _gunList.Add(gun);
            }
        }
    }

    private Gun FindGun(string name)
    {
        return _gunList.FirstOrDefault(gun => gun.Name == name);
    }
}
