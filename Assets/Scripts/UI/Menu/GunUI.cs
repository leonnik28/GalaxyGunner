using TMPro;
using UnityEngine;

public class GunUI : MonoBehaviour
{
    [SerializeField] private GunInventory _gunInventory;
    [SerializeField] private bool _isShop;

    [Space]
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _damage;
    [SerializeField] TextMeshProUGUI _rateOfFire;
    [SerializeField] TextMeshProUGUI _distance;
    [SerializeField] TextMeshProUGUI _cost;

    private void OnEnable()
    {
        if (_isShop)
        {
            _gunInventory.OnGunChanged += UpdateShopUI;
        }
        else
        {
            _gunInventory.OnGunChanged += UpdateInventoryUI;
        }
    }

    private void OnDisable()
    {
        if (_isShop)
        {
            _gunInventory.OnGunChanged -= UpdateShopUI;
        }
        else
        {
            _gunInventory.OnGunChanged -= UpdateInventoryUI;
        }
    }

    private void UpdateInventoryUI(Gun gun)
    {
        UpdateUI(gun);
    }

    private void UpdateShopUI(Gun gun)
    {
        UpdateUI(gun);
        _cost.text = "Cost: " + gun.Cost.ToString();
    }

    private void UpdateUI(Gun gun)
    {
        _name.text = "Name: " + gun.Name;
        _damage.text = "Damage: " + gun.Damage.ToString();
        _rateOfFire.text = "Rate of fire: " + gun.RateOfFire.ToString();
        _distance.text = "Distance: " + gun.Distance.ToString();
    }
}