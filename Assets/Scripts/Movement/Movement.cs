using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public MovementState CurrentState { get; private set; }

    public float NormalizedSpeed => _currentMoveVector.magnitude;
    public bool IsGrounded => _isGrounded;

    [Header("Move")]
    [SerializeField] private float _forwardSpeed = 5f;
    [SerializeField] private float _strafeSpeed = 5f;

    [Space]
    [SerializeField] private float _forwardAcceleration = 5f;
    [SerializeField] private float _strafeAcceleration = 5f;

    [Header("Jump")]
    [SerializeField] private float _fallMultiplier = 1.98f;
    [SerializeField] private float _jumpHeight = 2f;

    [Header("Wall Jump")]
    [SerializeField] private float _wallJumpHeight = 1f;
    [SerializeField] private float _wallJumpNormalHeight = 2f;
    [SerializeField] private float _wallMoveRiseUpHeight = 5f;
    [SerializeField] private float _wallFallMultiplier = .05f;

    [Header("Ground Detect")]
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundDetectDistance = 1f;

    [Header("Wall Detect")]
    [SerializeField] private LayerMask _wallLayerMask;
    [SerializeField, Min(0)] private float _wallSlowdownDistance = 2f;
    [SerializeField, Min(0)] private float _wallDetectDistance = 1f;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private float _surfaceAngle;
    [SerializeField] private float _moveSpeedHorizontal;
    [SerializeField] private float _moveSpeedForward;
    [SerializeField] private bool _isGroundedDebug;
    [SerializeField] private bool _isWallDebug;

    private CharacterController _characterControllerDebug;
#endif

    private CharacterController _characterController;

    private Vector3 _velocity;
    private Vector3 _moveDirection;
    private Vector3 _currentMoveVector;
    private Vector3 _surfaceNormal;
    private Vector3 _wallDirection;

    private bool _isGrounded
    {
        get
        {
            return m_isGrounded;
        }
        set 
        {
            if (value != m_isGrounded)
            {
                SetMovementState(value, m_isWalled);
            }

            m_isGrounded = value;
        }
    }
    private bool _isWalled
    {
        get
        {
            return m_isWalled;
        }
        set
        {
            if (value != m_isWalled)
            {
                SetMovementState(m_isGrounded, value);
            }

            m_isWalled = value;
        }
    }

    private bool m_isGrounded;
    private bool m_isWalled;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        CurrentState = new AirMoveState(_characterController, _fallMultiplier);
    }

    private void FixedUpdate()
    {
        _isWalled = GetWallsDetect(Vector3.left) || GetWallsDetect(Vector3.right);
        _isGrounded = GetGroundDetect(out RaycastHit groundHit);

        if (CurrentState.State == MovementState.PlaceState.Ground)
        {
            _wallDirection = Vector3.MoveTowards(
                _wallDirection,
                Vector3.zero,
                Time.fixedDeltaTime);
        }

#if UNITY_EDITOR
        _isGroundedDebug = _isGrounded;
        _isWallDebug = _isWalled;
#endif

        if (_isGrounded)
        {
            _surfaceNormal = groundHit.normal;

#if UNITY_EDITOR
            _surfaceAngle = Vector3.Angle(_surfaceNormal, Vector3.up);
#endif
        }
    }

    public void Move(float horizontalInput)
    {
        horizontalInput *= Mathf.Clamp01(Mathf.Abs(horizontalInput - _wallDirection.x));

        _moveDirection = (transform.forward * _forwardSpeed)
            + (transform.right * horizontalInput * _strafeSpeed);

        _moveSpeedForward = _moveDirection.z;

        var projectedNormal = GetProjectedNormal(_moveDirection);

        _currentMoveVector.x = Mathf.MoveTowards(
            _currentMoveVector.x,
            projectedNormal.x,
            _strafeAcceleration * Time.deltaTime);

        _currentMoveVector.z = Mathf.MoveTowards(
            _currentMoveVector.z,
            projectedNormal.z,
            _forwardAcceleration * Time.deltaTime);

        _velocity += CurrentState.GetFallVelocity() * Time.deltaTime;

        CurrentState.Move(_currentMoveVector, _velocity);

#if UNITY_EDITOR
        _moveSpeedHorizontal = _currentMoveVector.x;
        _moveSpeedForward = _currentMoveVector.z;
#endif
    }

    public void Jump()
    {
         _velocity = CurrentState.GetJumpVelocity(_surfaceNormal, _velocity);
    }

    private void SetMovementState(bool isGrounded, bool isWalled)
    {
        switch ((isGrounded, isWalled))
        {
            case (false, false):
                CurrentState = new AirMoveState(_characterController, _fallMultiplier);
                break;
            case (true, true):
                CurrentState = new WallGroundMoveState(_characterController, _wallMoveRiseUpHeight, _fallMultiplier);
                break;
            case (false, true):
                CurrentState = new WallMoveState(_characterController, _wallJumpNormalHeight, _wallJumpHeight, _wallFallMultiplier);
                break;
            case (true, false):
                CurrentState = new GroundMoveState(_characterController, _jumpHeight, _fallMultiplier);
                break;
        }
    }

    private Vector3 GetProjectedNormal(Vector3 direction)
    {
        return direction
            - Vector3.Dot(
                direction,
                _surfaceNormal) * _surfaceNormal;
    }

    private bool GetGroundDetect(out RaycastHit raycastHit)
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            out raycastHit,
            _groundDetectDistance,
            _groundLayerMask);
    }

    private bool GetWallsDetect(Vector3 direction)
    {
        if (Physics.Raycast(
            transform.position + _characterController.center + direction * _characterController.radius,
            direction,
            out RaycastHit raycastHit,
            _wallSlowdownDistance,
            _wallLayerMask))
        {
            _wallDirection = direction - direction * 
                (raycastHit.distance / _wallSlowdownDistance);

            if (raycastHit.distance < _wallDetectDistance)
            {
                if (_isGrounded == false)
                {
                    _surfaceNormal = raycastHit.normal;
                }

                return true;
            }
        }

        return false;
    }

    public void ResetMovement(Vector3 oldPosition)
    {
        transform.position = oldPosition;
        _moveSpeedForward = 0f;
        _currentMoveVector = Vector3.zero;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _characterControllerDebug = GetComponent<CharacterController>();

        if (_wallSlowdownDistance <= _wallDetectDistance)
        {
            _wallSlowdownDistance = _wallDetectDistance + .1f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position
            + GetProjectedNormal(
                transform.forward)
            * 2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * _groundDetectDistance);
    }
#endif
}
