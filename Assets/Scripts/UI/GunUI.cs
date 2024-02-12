using TMPro;
using UnityEngine;

public class GunUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    [Space]
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _damage;
    [SerializeField] TextMeshProUGUI _rateOfFire;
    [SerializeField] TextMeshProUGUI _distance;

    private void OnEnable()
    {
        _inventory.OnGunChanged += UpdateUI;
    }

    private void OnDisable()
    {
        _inventory.OnGunChanged -= UpdateUI;
    }

    private void UpdateUI(Gun gun)
    {
        _name.text = "Name: " + gun.Name;
        _damage.text = "Damage: " + gun.Damage.ToString();
        _rateOfFire.text = "Rate of fire: " + gun.RateOfFire.ToString();
        _distance.text = "Distance: " + gun.Distance.ToString();
    }
}