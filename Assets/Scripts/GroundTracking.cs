using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundTracking : MonoBehaviour
{
    private int _countGround;
    private bool _isGround;

    public int CountGround
    {
        get
        {
            return _countGround;
        }
        private set
        {
            _countGround = Mathf.Clamp(value, 0, int.MaxValue);

            if (_countGround == 0)
            {
                if (IsGround)
                    IsGround = false;
            }
            else
            {
                if (!IsGround)
                    IsGround = true;
            }
        }
    }

    public bool IsGround
    {
        get
        {
            return _isGround;
        }
        private set
        {
            _isGround = value;
            if (_isGround)
                GroundFound?.Invoke();
            else
                GroundLost?.Invoke();
        }
    }

    public event UnityAction GroundFound;
    public event UnityAction GroundLost;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Ground>(out Ground ground))
        {
            CountGround++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Ground>(out Ground ground))
        {
            CountGround--;
        }
    }
}
