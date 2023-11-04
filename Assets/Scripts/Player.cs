using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private Vector2 m_move;

    private void Update()
    {
        _movement.Move(m_move.x);
    }

    private void OnMove(InputValue value)
    {
        m_move = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _movement.Jump();
        }
    }
}
