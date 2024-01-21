using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;

    public void GetDamage(float damage)
    {
        _health -= damage;
        if( _health <= 0)
        {
            Destroy(gameObject);
        }
    }
}