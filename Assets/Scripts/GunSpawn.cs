using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunSpawn : MonoBehaviour
{
    public Animator GunAnimator => _animator;

    [SerializeField] private Inventory _inventory;

    private Animator _animator;
    private Gun _gun;

    public void Spawn()
    {
        GameObject gun = Instantiate(_gun.GunTransform.gameObject, _gun.Position, _gun.Rotation, transform);
        _animator = gun.GetComponent<Animator>();
    }

    public void ChooseGun()
    {
        _gun = _inventory.Gun;
        _inventory.gameObject.SetActive(false);
    }
}
