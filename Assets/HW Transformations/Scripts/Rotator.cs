using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private Vector3 _direction;

    private void Update()
    {
        transform.RotateAround(transform.position, _direction, _rotationSpeed * Time.deltaTime);
    }
}
