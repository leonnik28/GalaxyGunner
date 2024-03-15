using UnityEngine;
using DG.Tweening;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject _aimExtern;
    [SerializeField] private GunSpawn _gunSpawn;

    [SerializeField] private float _scaleCount = 1.0f;

    private readonly float _fireRateScaleFactor = 2000f;

    public void ScaleAim()
    {
        _aimExtern.transform.DOScale(transform.localScale * _scaleCount, _gunSpawn.Gun.RateOfFire / _fireRateScaleFactor).SetLoops(2, LoopType.Yoyo).SetUpdate(true);
    }
}
