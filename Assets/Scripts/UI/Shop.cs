using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Gun _gun;

    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _damage;
    [SerializeField] TextMeshProUGUI _rateOfFire;
    [SerializeField] TextMeshProUGUI _distance;
    [SerializeField] TextMeshProUGUI _cost;

    public void ChoiceGunOnShop()
    {
        _name.text = "Name: " + _gun.Name;
        _damage.text = "Damage: " + _gun.Damage.ToString();
        _rateOfFire.text = "Rate of fire: " + _gun.RateOfFire.ToString();
        _distance.text = "Distance: " + _gun.Distance.ToString();
        _cost.text = "Cost: " + _gun.Cost.ToString();
    }
}
