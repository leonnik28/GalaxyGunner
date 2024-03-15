using System.Threading.Tasks;
using UnityEngine;

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

    private readonly string _gunFilename = "currentGun";

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
        _gun = _gunInventory.GunInInventory;

        if (_gun != null)
        {
            _storageService.SaveAsync(_gunFilename, _gun.Index);
        }
    }

    public async Task LoadCurrentGun()
    { 
        int gunIndex = await _storageService.LoadAsync<int>(_gunFilename);
        _gun = _gunPool.GetGun(gunIndex);

        if (_gun == null)
        {
            _gun = _gunPool.GetGun(0);
        }
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
