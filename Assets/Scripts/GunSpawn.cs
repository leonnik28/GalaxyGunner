using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunSpawn : MonoBehaviour
{
    public Animator GunAnimator => _animator;

    [SerializeField] private Gun _gun;

    private Animator _animator;

    private void Start()
    {
        GameObject gun = Instantiate(_gun.GunTransform.gameObject, _gun.Position, _gun.Rotation, transform);
        _animator = gun.GetComponent<Animator>();
    }
}
