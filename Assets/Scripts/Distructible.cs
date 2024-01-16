using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distructible : MonoBehaviour
{
    [SerializeField] private GameObject _destroyObject;

    private void Destroy()
    {
        Instantiate(_destroyObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
