using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        Pistol,
        AssaultRifle,
        Weapon
    }
    public GunType Type;

    public float Damage => _damage;
    public float Distance => _distance;
    public int RateOfFire => _rateOfFire;
    public Animator GunAnimator => _gunAnimator;

    [SerializeField] private Transform _pistol;
    [SerializeField] private Transform _assaultRifle;
    [SerializeField] private Transform _weapon;

    private float _damage;
    private float _distance;
    private int _rateOfFire;
    private Animator _gunAnimator;

    private Vector3 _position = new Vector3(0f, 2f, 1.37f);
    private Quaternion _rotation = Quaternion.Euler(-95f, 0f, 0f);

    private void Start()
    {
        ChoicePistol();
    }

    public void ChoicePistol()
    {
        Type = GunType.Pistol;
        UpdateGun();
    }

    public void ChoiceAssaultRifle()
    {
        Type = GunType.AssaultRifle;
        UpdateGun();
    }

    public void ChoiceWeapon()
    {
        Type = GunType.Weapon;
        UpdateGun();
    }

    private void UpdateGun()
    {
        switch (Type)
        {
            case GunType.Pistol:
                GameObject pistol = Instantiate(_pistol.gameObject,  _position,  _rotation,  transform);
                _damage = 2f;
                _distance = 12f;
                _rateOfFire = 1000;
                _gunAnimator = pistol.GetComponent<Animator>();
                break;
              
            case GunType.AssaultRifle:
                GameObject assaultRifle = Instantiate(_assaultRifle.gameObject, _position, _rotation, transform);
                _damage = 3f;
                _distance = 15f;
                _rateOfFire = 500;
                _gunAnimator = assaultRifle.GetComponent<Animator>();
                break; 

            case GunType.Weapon:
                GameObject weapon = Instantiate(_weapon.gameObject, _position, _rotation, transform);
                _damage = 10f;
                _distance = 3f;
                _rateOfFire = 1500;
                _gunAnimator = weapon.GetComponent<Animator>();
                break;
        }
    }
}