using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }
}
