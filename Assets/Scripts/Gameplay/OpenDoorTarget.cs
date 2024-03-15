using UnityEngine;

public class OpenDoorTarget : MonoBehaviour, ITargetToOpenDoor
{
    [SerializeField] private Animator _openDoorAnimator;
    [SerializeField] private bool _isLeft;

    private Animator _animator;

    public void GetOpenDoor()
    {
        _animator = GetComponent<Animator>();

        if (_isLeft)
        {
            _animator.SetTrigger("DeathObjectLeft");
        }
        else
        {
            _animator.SetTrigger("DeathObjectRight");
        }

        _openDoorAnimator.SetBool("OpenDoor", true);
    }
}
