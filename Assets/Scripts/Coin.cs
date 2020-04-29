using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public event UnityAction<Point> CoinFound;

    private  Point _position;

    public void SetPosition(Point position)
    {
        _position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            CoinFound?.Invoke(_position);
            Destroy(gameObject);
        }
    }
}
