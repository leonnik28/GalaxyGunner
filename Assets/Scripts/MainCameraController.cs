using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [SerializeField] private Movement _movement;
    [SerializeField] private Rigidbody _player;

    [Space]
    [SerializeField] private float _tiltAmount = 0.3f;
    [SerializeField] private float _maxRotation = 30f;

    [Header("FOV")]
    [SerializeField] private float _minFOV = 60f;
    [SerializeField] private float _maxFOV = 70f;
    [SerializeField] private Animator _moveAnimator;

    private Vector3 _lastPlayerPosition;
    private float _originalTilt;

    private void Start()
    {
        _vcam.Follow = _player.transform;
        _lastPlayerPosition = _player.transform.position;
        _originalTilt = _vcam.transform.localRotation.eulerAngles.x;
    }

    private void Update()
    {
        if (_movement.CurrentState.State == MovementState.PlaceState.Ground ||
            _movement.CurrentState.State == MovementState.PlaceState.WallGround)
        {
            OnLanding();
        }
        else
        {
            OnJump();
        }

        float newFOV = _minFOV + _player.velocity.magnitude;
        _vcam.m_Lens.FieldOfView = Mathf.Clamp(newFOV, _minFOV, _maxFOV);
        var move = _player.transform.position - _lastPlayerPosition;      

        if (_movement.CurrentState.State != MovementState.PlaceState.WallGround)
        {
            var tilt = _originalTilt + move.x * _tiltAmount;
            tilt = Mathf.Clamp(tilt, -1, 1);
            _vcam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);

            float rotationSpeed = 20;
            float rotationAmount = move.x * Time.deltaTime * rotationSpeed;
            rotationAmount = Mathf.Clamp(rotationAmount, -_maxRotation, _maxRotation);
            _vcam.transform.RotateAround(_player.transform.position, Vector3.up, rotationAmount);
        }

        _lastPlayerPosition = _player.transform.position;
    }

    public void OnLanding()
    {
        _moveAnimator.speed = 1;
    }

    public void OnJump()
    {
        _moveAnimator.speed = 0;
    }
}