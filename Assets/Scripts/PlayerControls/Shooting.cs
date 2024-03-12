using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GunSpawn _gunSpawn;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private Aim _aim;

    private Camera _camera;
    private Gun _gun;

    private bool _isDelayInProgress = false;
    private bool _gameIsStart = false;

    private void Update()
    {
        if (_gameIsStart && _updateGameLevel.IsGameActive)
        {
            Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);
            TryShoot(ray);
        }
    }

    public void GameStart()
    {
        _gun = _gunSpawn.Gun;
        _camera = Camera.main;
        _gameIsStart = true;
    }

    public void GameStop()
    {
        _gameIsStart = false;
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
        if (!_isDelayInProgress)
        {
            _isDelayInProgress = true;

            if (hit.transform.TryGetComponent(out IDamageable damageable))
            {
                StartFire();
                damageable.GetDamage(_gun.Damage);
                await Task.Delay(_gun.RateOfFire);
            }
            else if (hit.transform.TryGetComponent(out ITargetToOpenDoor openDoor))
            {
                StartFire();
                openDoor.GetOpenDoor();
                await Task.Delay(_gun.RateOfFire);
            }

            _isDelayInProgress = false;
        }
    }

    private void StartFire()
    {
        _gunSpawn.GunAnimator.SetTrigger("StartFireAnimation");
        _gunSpawn.GunAudioSource.PlayOneShot(_gunSpawn.GunAudioSource.clip);
        _aim.ScaleAim();
    }
}

