using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject _objectUI;

    public void OnClickExit()
    {
        if (_objectUI != null)
        {
            _objectUI.SetActive(false);
        }
    }
}
