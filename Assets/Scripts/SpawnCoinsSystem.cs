using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinsSystem : MonoBehaviour
{    
    private List<Point> _availableSpawnPoints;

    [SerializeField] private int _countCoin;
    [SerializeField] private Coin _coin;

    private void Start()
    {
        _availableSpawnPoints = new List<Point>();
        Point[] _allSpawnPoint = GetComponentsInChildren<Point>();

        for (int i = 0; i < _allSpawnPoint.Length; i++)
        {
            _availableSpawnPoints.Add(_allSpawnPoint[i]);
        }

        StartCoroutine(CreateCoinInRandomPoint());
    }   
    
    private void CoinFound(Point position)
    {
        _availableSpawnPoints.Add(position);
        _countCoin++;
    }

    private IEnumerator CreateCoinInRandomPoint()
    {
        var timer = new WaitForSeconds(5f);

        while (true)
        {
            if (_availableSpawnPoints.Count > 0)
            {
                Point point = _availableSpawnPoints[Random.Range(0, _availableSpawnPoints.Count)];
                Coin tempCoin = Instantiate(_coin, point.transform);
                tempCoin.SetPosition(point);
                tempCoin.CoinFound += CoinFound;

                _availableSpawnPoints.Remove(point);
            }

            yield return timer;
        }
    }
}
