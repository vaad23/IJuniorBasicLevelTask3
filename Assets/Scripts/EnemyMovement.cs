using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Point[] _wayPoints;
    private int _currentPoint;

    private void Start()
    {
        _wayPoints = _path.GetComponentsInChildren<Point>();
        if(_wayPoints.Length > 1)
        {
            transform.position = _wayPoints[0].transform.position;
            _currentPoint = 1;
        }
    }

    private void Update()
    {
        if (_wayPoints.Length > 1)
        {
            Vector3 targetPosition = _wayPoints[_currentPoint].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            if(transform.position == targetPosition)
            {
                _currentPoint++;

                if (_currentPoint >= _wayPoints.Length)
                    _currentPoint = 0;
            }
        }
    }
}
