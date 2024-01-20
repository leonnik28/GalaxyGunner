using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeechActivator : MonoBehaviour
{
    [SerializeField] private Animator _teechAnimator;
    [SerializeField] private Vector3 _numberAnimation;

    private bool _inInsideCollider = false;

    private void Start()
    {
        _teechAnimator.Play("Empty");
    }

    public void StartAnimation()
    {
        if(_numberAnimation.x == 1)
        {
            _teechAnimator.SetTrigger("StartLeftAnimation");
        }
        if(_numberAnimation.y == 1)
        {
            _teechAnimator.SetTrigger("StartMiddleAnimation");
        }
        if(_numberAnimation.z == 1)
        {
            _teechAnimator.SetTrigger("StartRightAnimation");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_inInsideCollider)
        {
            _inInsideCollider = true;
            StartAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _inInsideCollider = false;
    }
}
