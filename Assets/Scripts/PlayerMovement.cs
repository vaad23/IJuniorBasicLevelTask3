using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundTracking groundTracking;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite;
    public State CurrentState { get; private set; }

    public enum State
    {
        Idle,
        Run,
        JumpUp,
        JumpDown
    }

    private void OnEnable()
    {
        groundTracking.Changed += StateChange;
    }

    private void OnDisable()
    {
        groundTracking.Changed -= StateChange;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
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
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Idle()
    {

    }

    private void Run(bool flipX)
    {
        int direction = flipX ? -1 : 1;
        if (_sprite.flipX != flipX)
            _sprite.flipX = flipX;

        transform.Translate(_speed * Time.deltaTime * direction, 0, 0);
    }

    private void StateChange(State state)
    {
        if (CurrentState != state)
        {
            CurrentState = state;
            switch (CurrentState)
            {
                case State.Idle:
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
}
