using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun", order = 51)]
public class Gun : ScriptableObject
{
    public float Damage => _damage;
    public float Distance => _distance;
    public int RateOfFire => _rateOfFire;
    public Transform GunTransform => _gunTransform;
    public Vector3 Position => _position;
    public Quaternion Rotation => _rotation;

    [SerializeField] private float _damage;
    [SerializeField] private float _distance;
    [SerializeField] private int _rateOfFire;
    [SerializeField] private Transform _gunTransform;

    [Space]
    [SerializeField] private Vector3 _position = new Vector3(0f, 2f, 1.37f);
    [SerializeField] private Quaternion _rotation = Quaternion.Euler(-95f, 0f, 0f);
}
