using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator _animator;

    public void GetDamage(float damage)
    {
        _animator.SetTrigger("GetDamage");
    }
}
