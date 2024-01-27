using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;

    private Animator _animator;

    public void GetDamage(float damage)
    {
        _animator = GetComponent<Animator>();
        if (_animator != null)
        {
            _animator.SetTrigger("GetDamage");
        }

        _health -= damage;
        if( _health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}