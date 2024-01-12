using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTarget : MonoBehaviour, ITargetToOpenDoor
{
    [SerializeField] private Animator _openDoorAnimator;

    public void GetOpenDoor()
    {
        _openDoorAnimator.SetBool("OpenDoor", true);
    }
}
