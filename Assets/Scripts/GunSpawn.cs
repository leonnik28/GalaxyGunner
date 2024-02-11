using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class GunSpawn : MonoBehaviour
{
    public Gun Gun => _gun;
    public Animator GunAnimator => _gunAnimator;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private GunPool _gunPool;
    [SerializeField] private IStorageService _storageService;

    private Gun _gun;
    private Animator _gunAnimator;

    private void Awake()
    {
        _storageService = new StorageService();
    }

    public async void Spawn()
    {
        await LoadCurrentGun();
        GameObject gun = Instantiate(_gun.GunTransform.gameObject, _gun.Position, _gun.Rotation, transform);
        _gunAnimator = gun.GetComponent<Animator>();
    }

    public void ChooseGun()
    {
        _gun = _inventory.Gun;
        _inventory.gameObject.SetActive(false);

        _storageService.SaveAsync("currentGun", _gun.Name);
    }

    public async Task LoadCurrentGun()
    { 
        string gunName = await _storageService.LoadAsync<string>("currentGun");
        _gun = _gunPool.GetGun(gunName);
    }
}
