using UnityEngine;

public class GroundMoveState : MovementState
{
    public override PlaceState State => PlaceState.Ground;

    private readonly float _jumpHeight;

    public GroundMoveState(CharacterController characterController, float jumpHeight, float fallMultiplier) : base(characterController, fallMultiplier)
    {
        _jumpHeight = jumpHeight;
    }

    public override Vector3 GetJumpVelocity(Vector3 surfaceNormal, Vector3 _)
    {
        return surfaceNormal * Mathf.Sqrt(_jumpHeight * -3.0f * Physics.gravity.y);
    }

    public override void Move(Vector3 moveVector, Vector3 velocity)
    {
        if (velocity.y < 0)
        {
            velocity = Vector3.zero;
        }

        _characterController.Move(moveVector * Time.deltaTime);
        _characterController.Move(velocity * Time.deltaTime);
    }
}
