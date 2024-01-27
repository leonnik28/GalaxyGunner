using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethSpawn : MonoBehaviour
{
    public Animator TeethAnimator => _teethAnimator;

    [SerializeField] private Transform _teeth;
    [SerializeField] private Vector3 _localPosition;
    [SerializeField] private Quaternion _localRotation;

    private GameObject _teethObject;
    private Animator _teethAnimator;

    private void Start()
    {
        if (_teethObject == null)
        {
            Vector3 globalPosition = transform.TransformPoint(_localPosition);
            Quaternion globalRotation = transform.rotation * _localRotation;
            _teethObject = Instantiate(_teeth.gameObject, globalPosition, globalRotation);
            _teethAnimator = _teethObject.GetComponent<Animator>();
        }
    }

}
