using UnityEngine;

public class WallMoveState : MovementState
{
    private float _wallJumpNormalHeight;
    private float _wallJumpHeight;
    private float _wallMoveRiseUp;

    public WallMoveState(
        CharacterController characterController,
        float wallJumpNormalHeight,
        float wallJumpHeight
        , float fallMultiplier) : base(characterController, fallMultiplier)
    {
        _wallJumpNormalHeight = wallJumpNormalHeight;
        _wallJumpHeight = wallJumpHeight;
    }

    public override PlaceState State => PlaceState.Wall;

    public override Vector3 GetJumpVelocity(Vector3 surfaceNormal, Vector3 _)
    {
        return surfaceNormal * Mathf.Sqrt(_wallJumpNormalHeight * -3.0f * Physics.gravity.y) +
            Vector3.up * Mathf.Sqrt(_wallJumpHeight * -3.0f * Physics.gravity.y);
    }

    public override void Move(Vector3 moveVector, Vector3 velocity)
    {
        _characterController.Move(moveVector * Time.deltaTime);
        _characterController.Move(velocity * Time.deltaTime);
    }
}
