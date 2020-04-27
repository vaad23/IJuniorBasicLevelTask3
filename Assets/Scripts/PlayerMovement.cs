using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundTracking _underfoot;
    [SerializeField] private GroundTracking _onLeft;
    [SerializeField] private GroundTracking _onRight;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Coroutine _stateTracking;

    public State CurrentState { get; private set; }

    public enum State
    {
        Idle = 0,
        Run = 1,
        JumpUp = 2,
        JumpDown = 3
    }

    private void OnEnable()
    {
      //  groundTracking.GroundFound += GroundFound;
        _underfoot.GroundLost += GroundLost;
    }

    private void OnDisable()
    {
      //  groundTracking.GroundFound -= GroundFound;
        _underfoot.GroundLost -= GroundLost;
    }

    private void GroundFound()
    {
        Debug.Log("GroundFound");
    }
    private void GroundLost()
    {
        StateTracking(true);
        Debug.Log("GroundLost");
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        CurrentState = State.Idle;        
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Run(false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Run(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_underfoot.IsGround)
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
    
    private void Run(bool flipX)
    {
        int direction = flipX ? -1 : 1;
        StateTracking(true);
        if (_sprite.flipX != flipX)
            _sprite.flipX = flipX;

        if (flipX)
        {
            if (_onLeft.IsGround)
                return;
        }
        else
        {
            if (_onRight.IsGround)
                return;
        }

        transform.Translate(_speed * Time.deltaTime * direction, 0, 0);
    }

    private void StateChange(State state)
    {
        if (CurrentState != state)
        {
            CurrentState = state;
            _animator.SetInteger("State", (int)state);
            switch (CurrentState)
            {
                case State.Idle:
                    StateTracking(false);
                    Debug.Log("Idle");
                    break;
                case State.Run:
                    Debug.Log("Run");
                    break;
                case State.JumpUp:
                    Debug.Log("Up");
                    break;
                case State.JumpDown:
                    Debug.Log("Down");
                    break;
            }
        }
    }

    private void StateTracking(bool activate)
    {
        if (activate)
        {
            if (_stateTracking == null)
                _stateTracking = StartCoroutine(StateTracking());
        }
        else
        {
            if (_stateTracking != null)
            {
                StopCoroutine(_stateTracking);
                _stateTracking = null;
            }
        }
    }

    private IEnumerator StateTracking()
    {
        Vector3 pastPosition = transform.position;
        while (true)
        {
            if (_underfoot.IsGround)
            {
                if (pastPosition.x != transform.position.x)
                {
                    pastPosition.x = transform.position.x;
                    StateChange(State.Run);
                }
                else
                {
                    StateChange(State.Idle);
                }
            }
            else
            {
                if (pastPosition.y - transform.position.y < -0.1)
                {
                    StateChange(State.JumpUp);
                    pastPosition = transform.position;
                }
                else if (pastPosition.y - transform.position.y > 0.1)
                {
                    StateChange(State.JumpDown);
                    pastPosition = transform.position;
                }
            }
            yield return null;
        }
    }
}
