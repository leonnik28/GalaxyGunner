using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private Vector2 _inputVector;

    private void Update()
    {
        _movement.Move(_inputVector.x);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _movement.Jump();
        }
    }

}
