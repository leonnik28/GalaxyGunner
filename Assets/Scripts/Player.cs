using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private Vector2 _inputVector;
    private float _inputScaleVector = 5f / 6f;

    private void Update()
    {
        _movement.Move(_inputVector.x);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>() * _inputScaleVector;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _movement.Jump();
        }
    }

}
