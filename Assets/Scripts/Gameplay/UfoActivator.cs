using UnityEngine;

public class UfoActivator : MonoBehaviour, IUfoActivator
{
    [SerializeField] private Animator _ufoAnimator;

    private bool _inInsideCollider = false;

    private void Start()
    {
        _ufoAnimator.Play("Empty");
    }

    public void GetLaser()
    {
        _ufoAnimator.SetTrigger("StartUFOAnimation");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_inInsideCollider)
        {
            _inInsideCollider = true;
            GetLaser();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _inInsideCollider = false;
    }
}
