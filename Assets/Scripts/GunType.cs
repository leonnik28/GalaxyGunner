using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunType
{
    public enum Type
    {
        Pistol,
        TommyGun,
        SniperRifle
    }
    public abstract Type TypeGun { get; }

    protected float damage;

}
