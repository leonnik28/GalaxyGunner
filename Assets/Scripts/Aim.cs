using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject _aimExtern;
    [SerializeField] private Gun _gun;
    [SerializeField] private float _scaleCount = 1.0f;

    private float _fireRateScaleFactor = 2000f;

    public void ScaleAim()
    {
        _aimExtern.transform.DOScale(transform.localScale * _scaleCount, (float)_gun.RateOfFire / _fireRateScaleFactor).SetLoops(2, LoopType.Yoyo);
    }

}
