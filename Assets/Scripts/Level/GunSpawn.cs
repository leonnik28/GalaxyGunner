using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GunSpawn : MonoBehaviour
{
    public Gun Gun => _gun;
    public Animator GunAnimator => _gunAnimator;
    public AudioSource GunAudioSource => _gunAudioSource;

    [SerializeField] private GunInventory _gunInventory;
    [SerializeField] private GunPool _gunPool;

    private IStorageService _storageService;

    private Gun _gun;
    private Animator _gunAnimator;
    private AudioSource _gunAudioSource;
    private GameObject _spawnedGun;

    private void Awake()
    {
        _storageService = new StorageService();
    }

    public async void Spawn()
    {
        await LoadCurrentGun();
        _spawnedGun = Instantiate(_gun.GunTransform.gameObject, _gun.Position, _gun.Rotation, transform);
        _gunAnimator = _spawnedGun.GetComponent<Animator>();
        _gunAudioSource = _spawnedGun.GetComponent<AudioSource>();
    }

    public void ChooseGun()
    {
        _gun = _gunInventory.Gun;

        if (_gun != null)
        {
            _storageService.SaveAsync("currentGun", _gun.Index);
        }
    }

    public async Task LoadCurrentGun()
    { 
        int gunIndex = await _storageService.LoadAsync<int>("currentGun");
        _gun = _gunPool.GetGun(gunIndex);
    }

    public void DeleteGun()
    {
        if (_spawnedGun != null)
        {
            Destroy(_spawnedGun);
            _spawnedGun = null;
            _gunAnimator = null;
            _gunAudioSource = null;
        }
    }
}
