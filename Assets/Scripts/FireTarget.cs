using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTarget : MonoBehaviour, IDamageable
{
    public void GetDamage()
    {
        Destroy(gameObject);
    }
}