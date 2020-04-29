using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundTracking : MonoBehaviour
{
    private int _countGround;

    public bool IsGround { get; private set; }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
        {
            CountGround++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
        {
            CountGround--;
        }
    }
}
