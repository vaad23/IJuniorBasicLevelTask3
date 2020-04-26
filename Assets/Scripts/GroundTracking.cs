using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundTracking : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Vector3 _playerPosition;
    [SerializeField] private Vector3 _pastPlayerPosition;
    private bool _isGround;

    public delegate void PlayerState(PlayerMovement.State state);
    public event PlayerState Changed;

    private void Start()
    {
        _isGround = true;
        _playerPosition = _player.gameObject.transform.position;
        _pastPlayerPosition = new Vector3(_playerPosition.x, _playerPosition.y, _playerPosition.z);
        /*
        _pastPlayerPosition.x = _playerPosition.x;
        _pastPlayerPosition.y = _playerPosition.y;
        _pastPlayerPosition.z = _playerPosition.z;*/
    }

    private void Update()
    {
        Debug.Log(_playerPosition.x - _pastPlayerPosition.x);
        Debug.Log(_playerPosition.y - _pastPlayerPosition.y);
        if (Vector3.Distance(_pastPlayerPosition, _playerPosition) > 0.01)
        {
            if (_isGround)
            {
                if (Mathf.Abs(_playerPosition.x - _pastPlayerPosition.x) > 0.01)
                    Changed?.Invoke(PlayerMovement.State.Run);
            }
            else
            {
                if (_playerPosition.y > _pastPlayerPosition.y)
                    Changed?.Invoke(PlayerMovement.State.JumpUp);
                else
                    Changed?.Invoke(PlayerMovement.State.JumpDown);
            }
        }
        else
        {
            if (_isGround)
                Changed?.Invoke(PlayerMovement.State.Idle);
            else
                Changed?.Invoke(PlayerMovement.State.JumpDown);
        }


        _playerPosition = _player.gameObject.transform.position;
        //_pastPlayerPosition = new Vector3(_playerPosition.x, _playerPosition.y, _playerPosition.z);
          if (Mathf.Abs(_playerPosition.x - _pastPlayerPosition.x) > 0.01)
              _pastPlayerPosition.x = _playerPosition.x;
           if (Mathf.Abs(_playerPosition.y - _pastPlayerPosition.y) > 0.01)
             _pastPlayerPosition.y = _playerPosition.y;
    }
}
