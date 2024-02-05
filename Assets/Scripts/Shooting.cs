using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private GunSpawn _gunSpawn;
    [SerializeField] private Aim _aim;

    private Camera _camera;

    private bool _isDelayInProgress = false;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);
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
                _gunSpawn.GunAnimator.SetTrigger("StartFireAnimation");
               // _gun.GunAudioSource.PlayOneShot(_gun.GunAudioSource.clip);
                _aim.ScaleAim();
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
                _gunSpawn.GunAnimator.SetTrigger("StartFireAnimation");
                //_gun.GunAudioSource.PlayOneShot(_gun.GunAudioSource.clip);
                _aim.ScaleAim();
                openDoor.GetOpenDoor();
                await Task.Delay(_gun.RateOfFire);

                _isDelayInProgress = false;
            }
        }
    }
}

