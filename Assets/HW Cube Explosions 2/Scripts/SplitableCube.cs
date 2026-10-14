using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class SplitableCube : MonoBehaviour
{
    [Header("Состояние куба")]
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1f;
    [SerializeField] private float _size = 1f;

    private Renderer _renderer;

    public float Size => _size;
    public float SplitChance => _splitChance;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(float size, float splitChance, Color color)
    {
        _size = size;
        _splitChance = splitChance;
        _renderer.material.color = color;
        transform.localScale = Vector3.one * _size;
    }
}
