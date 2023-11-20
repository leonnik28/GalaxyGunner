using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class MainCameraController : MonoBehaviour
{
    [Space]
    [SerializeField] private CinemachineVirtualCamera _vcam;

    [Space]
    [SerializeField] private Rigidbody _player;

    [Space]
    [SerializeField] private float _tiltAmount = 0.3f;

    [Space]
    [SerializeField] private float _maxRotation = 30f;

    [Header("FOV")]
    [SerializeField] private float _minFOV = 60f;
    [SerializeField] private float _maxFOV = 70f;

    private Vector3 _lastPlayerPosition;
    private float _originalTilt;
    private Vector2 _moveInput;

    void Start()
    {
        _vcam.Follow = _player.transform;
        _lastPlayerPosition = _player.transform.position;
        _originalTilt = _vcam.transform.localRotation.eulerAngles.x;
    }

    void Update()
    {
        float newFOV = _minFOV + _player.velocity.magnitude;
        _vcam.m_Lens.FieldOfView = Mathf.Clamp(newFOV, _minFOV, _maxFOV);

        RaycastHit hit = new RaycastHit();

        var move = _player.transform.position - _lastPlayerPosition;

        bool isMovingTowardsWall = Vector3.Dot(move, hit.normal) < 0;

        bool isTouchingWall = Physics.Raycast(_player.transform.position, Vector3.right * _moveInput.x, out hit, 1f) &&
            hit.collider.GetComponent<IWall>() != null &&
            hit.collider.GetComponent<IWall>().IsWall() &&
            isMovingTowardsWall;


        if (!isTouchingWall)
        {
            var tilt = _originalTilt + move.x * _tiltAmount;
            tilt = Mathf.Clamp(tilt, -1, 1);
            _vcam.transform.localRotation = Quaternion.Euler(tilt, 0, 0);

            float rotationSpeed = 20;
            _vcam.transform.RotateAround(_player.transform.position, Vector3.up, move.x * Time.deltaTime * rotationSpeed);
        }


        _lastPlayerPosition = _player.transform.position;
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
}