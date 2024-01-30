using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTarget : MonoBehaviour, ITargetToOpenDoor
{
    [SerializeField] private Animator _openDoorAnimator;
    [SerializeField] private bool _isLeft;

    private Animator _animator;

    public void GetOpenDoor()
    {
        _animator = GetComponent<Animator>();
        if(_isLeft)
        {
            _animator.SetBool("DeathObjectLeft", true);
        }
        else
        {
            _animator.SetBool("DeathObjectRight", true);
        }

        _openDoorAnimator.SetBool("OpenDoor", true);
    }
}
