using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPool : MonoBehaviour
{
    [SerializeField] private List<Gun> _gunList;

    public Gun GetGun(string gunName)
    {
        foreach (Gun gun in _gunList)
        {
            if(gun.name == gunName)
            {
                return gun;
            }
        }

        return null;
    }
}
