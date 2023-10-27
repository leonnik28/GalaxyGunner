using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private float _horizontalInput;

    private void Update()
    {
        _movement.Move(_horizontalInput);
    }
}
