using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _speed = 60;
    [SerializeField] private Vector3 _maxScale;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _maxScale, _speed * Time.deltaTime);
    }
}
