using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] Animator _handsAnimator;

    private Vector2 _inputVector;
    private AudioSource _gameMusic;
    private float _inputScaleVector = 5f / 6f;
    private bool _gameIsStart = false;

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
        SetMusic(true);
    }

    public void GameStop()
    {
        _gameIsStart = false;
        SetMusic(false);
    }

    public void SetMusic(bool play, bool atFirst = true)
    {
        if(atFirst)
        {
            if (play)
            {
                _gameMusic.Play();
                _gameMusic.mute = false;
            }
            else
            {
                _gameMusic.Stop();
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
