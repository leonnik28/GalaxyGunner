using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Aim : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _scaleCount = 1.0f;

    public void ScaleObject()
    {
        transform.DOScale(transform.localScale * _scaleCount, _duration).SetLoops(2, LoopType.Yoyo);
    }
}
