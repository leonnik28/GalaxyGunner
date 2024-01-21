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

    public Animator GunAnimator;

    [SerializeField] private Transform _pistol;
    [SerializeField] private Transform _assaultRifle;
    [SerializeField] private Transform _weapon;

    private float _damage;
    private float _distance;
    private int _rateOfFire;

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
        GunAnimator = _assaultRifle.GetComponent<Animator>();
        UpdateGun();
    }

    public void ChoiceWeapon()
    {
        Type = GunType.Weapon;
        GunAnimator = _weapon.GetComponent<Animator>();
        UpdateGun();
    }

    private void UpdateGun()
    {
        switch (Type)
        {
            case GunType.Pistol:
                GameObject pistol = Instantiate(_pistol.gameObject,  _position,  _rotation,  transform);
                _damage = 1f;
                _distance = 5f;
                _rateOfFire = 2000;
                GunAnimator = pistol.GetComponent<Animator>();
                break;
              
            case GunType.AssaultRifle:
                GameObject assaultRifle = Instantiate(_assaultRifle.gameObject, _position, _rotation, transform);
                _damage = 2f;
                _distance = 6f;
                _rateOfFire = 500;
                break; 

            case GunType.Weapon:
                GameObject weapon = Instantiate(_weapon.gameObject, _position, _rotation, transform);
                _damage = 5f;
                _distance = 2f;
                _rateOfFire = 4000;
                break;
        }
    }
}