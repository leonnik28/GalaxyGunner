using UnityEngine;

public abstract class MovementState
{
    public enum PlaceState
    {
        Air,
        Wall,
        Ground,
        WallGround
    }
    public abstract PlaceState State { get; }

    private protected readonly CharacterController _characterController;

    private readonly float _fallMultiplier;

    public MovementState(CharacterController characterController, float fallMultiplier)
    {
        _characterController = characterController;

        _fallMultiplier = fallMultiplier;
    }

    public abstract void Move(Vector3 moveVector, Vector3 velocity);
    public abstract Vector3 GetJumpVelocity(Vector3 surfaceNormal, Vector3 currentVelocity);
    public virtual Vector3 GetFallVelocity()
    {
        return Physics.gravity * _fallMultiplier;
    }

}
