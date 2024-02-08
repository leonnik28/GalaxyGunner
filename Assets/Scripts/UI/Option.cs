using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject _options;

    public void OnOptionsClick()
    {
        _options.SetActive(true);
    }
}
