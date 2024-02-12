using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Gun Gun => _gun;

    public event Action<Gun> OnGunChanged;

    [SerializeField] private List<Gun> _gunList;

    private Gun _gun;

    public void ChooseGun(int index)
    {
        _gun = _gunList[index];
        OnGunChanged?.Invoke(_gun);
    }
}
