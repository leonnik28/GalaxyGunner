using UnityEngine;

public class TraveledDistance : MonoBehaviour
{
    public float Distance { get; private set; }

    [SerializeField] private Transform _root;

    private float _startPositionZ;

    private void Start()
    {
        _startPositionZ = _root.position.z;
    }

    private void LateUpdate()
    {
        Distance = _root.position.z - _startPositionZ;
    }
}
