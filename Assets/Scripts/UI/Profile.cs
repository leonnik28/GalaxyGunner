using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private GameObject _profile;

    public void OnProfileClick()
    {
        _profile.SetActive(true);
    }
}
