using UnityEngine;

public class AirMoveState : MovementState
{
    public AirMoveState(CharacterController characterController, float fallMultiplier) : base(characterController, fallMultiplier)
    { }

    public override PlaceState State => PlaceState.Air;

    public override Vector3 GetJumpVelocity(Vector3 _, Vector3 currentVelovity)
    {
        return currentVelovity;
    }

    public override void Move(Vector3 moveVector, Vector3 velocity)
    {
        _characterController.Move(moveVector * Time.deltaTime);
        _characterController.Move(velocity * Time.deltaTime);
    }
}
