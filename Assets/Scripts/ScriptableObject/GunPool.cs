using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPool : MonoBehaviour
{
    [SerializeField] private List<Gun> _gunList;

    public Gun GetGun(int gunIndex)
    {
        foreach (Gun gun in _gunList)
        {
            if(gun.Index == gunIndex)
            {
                return gun;
            }
        }

        return null;
    }
}
