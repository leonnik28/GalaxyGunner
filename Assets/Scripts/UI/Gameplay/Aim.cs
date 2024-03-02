using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject _aimExtern;
    [SerializeField] private float _scaleCount = 1.0f;
    [SerializeField] private GunSpawn _gunSpawn;

    private float _fireRateScaleFactor = 2000f;
    private Vector3 _oldTransformScale;

    private void Start()
    {
        _oldTransformScale = transform.localScale;
    }

    public void ScaleAim()
    {
        _aimExtern.transform.DOScale(transform.localScale * _scaleCount, (float)_gunSpawn.Gun.RateOfFire / _fireRateScaleFactor).SetLoops(2, LoopType.Yoyo).SetUpdate(true);
    }
}
