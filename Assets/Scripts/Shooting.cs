using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Animator _fireGunAnimator;
    [SerializeField] private float _radius;
    [SerializeField] private Gun _gun;

    private Camera _camera;

    private bool _isDelayInProgress = false;

    private void Awake()
    {
        _camera = Camera.main;
        
    }

    private void FixedUpdate()
    {
        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        TryShoot(ray);
    }

    private void TryShoot(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, _gun.Distance))
        {
            TryHitEnemy(hit);
        }
    }

    private async void TryHitEnemy(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IDamageable damageable))
        {
            if (!_isDelayInProgress)
            {
                _isDelayInProgress = true;
                _gun.GunAnimator.SetTrigger("StartFireAnimation");
                damageable.GetDamage(_gun.Damage);

                await Task.Delay(_gun.RateOfFire);
                _isDelayInProgress = false;
            }
        }
        else if (hit.transform.TryGetComponent(out ITargetToOpenDoor openDoor))
        {
            if (!_isDelayInProgress)
            {
                _isDelayInProgress = true;
                _gun.GunAnimator.SetTrigger("StartFireAnimation");
                openDoor.GetOpenDoor();
                await Task.Delay(_gun.RateOfFire);

                _isDelayInProgress = false;
            }
        }
    }
}

