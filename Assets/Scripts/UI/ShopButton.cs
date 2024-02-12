using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _shopButton;

    public void OnShopClick()
    {
        _shopButton.SetActive(true);
    }
}
