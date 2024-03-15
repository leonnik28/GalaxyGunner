using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public AudioSource GameMusic => _gameMusic;

    [SerializeField] private Movement _movement;
    [SerializeField] Animator _handsAnimator;

    private Vector2 _inputVector;
    private AudioSource _gameMusic;
    private bool _gameIsStart = false;

    private readonly float _inputScaleVector = 5f / 6f;

    private void Awake()
    {
        _gameMusic = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_gameIsStart)
        {
            _movement.Move(_inputVector.x);
        }
    }

    public void GameStart()
    {
        _gameIsStart = true;
        _handsAnimator.SetTrigger("StartAnimation");
        if (!_gameMusic.mute)
        {
            SetMusic(true);
        }
    }

    public void GameStop()
    {
        _gameIsStart = false;
    }

    public void SetMusic(bool play, bool isFirstTime = true)
    {
        if (isFirstTime)
        {
            if (play)
            {
                _gameMusic.Play();
            }
            else
            {
                _gameMusic.Stop();
                _gameMusic.mute = false;
            }
        }
        else
        {
            if (play)
            {
                _gameMusic.mute = false;
            }
            else
            {
                _gameMusic.mute = true;
            }
        }
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
