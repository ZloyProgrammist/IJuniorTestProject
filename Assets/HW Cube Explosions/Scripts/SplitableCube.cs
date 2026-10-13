using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class SplitableCube : MonoBehaviour
{
    [Header("Состояние куба")]
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1f;
    [SerializeField] private float _size = 1f;

    [Header("Настройки разделения")]
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField] private float _spawnScaleFactor = 0.5f;
    [SerializeField] private float _explotionForce = 5f;
    [SerializeField] private float _spawnOffset = 0.6f;
    [SerializeField, Min(1f)] private float _chanceDecreaseDivider = 2f;

    [Header("Цвет нового куба (HSV)")]
    [SerializeField, Range(0f, 1f)] private float _hueMin = 0f;
    [SerializeField, Range(0f, 1f)] private float _hueMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _saturationMin = 0.6f;
    [SerializeField, Range(0f, 1f)] private float _saturationMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _valueMin = 0.6f;
    [SerializeField, Range(0f, 1f)] private float _valueMax = 1f;

    private Renderer _renderer;

    public event Action<SplitableCube> CubeSplit;
    public event Action<SplitableCube> CubeDisappeared;

    public void Initialize(float size, float splitChance, Color color)
    {
        _size = size;
        _splitChance = splitChance;
        _renderer.material.color = color;
        transform.localScale = Vector3.one * _size;
    }

    private void OnCubeClicked(SplitableCube cube)
    {
        if (cube != this) 
            return;

        if (Random.value <= _splitChance)
        {
            Split();
        }
        else
        {
            CubeDisappeared?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        var handler = CubeClickHandler.Instance;

        if (handler != null)
        {
            handler.CubeClicked += OnCubeClicked;
        }
        else
        {
            Debug.LogWarning($"{nameof(SplitableCube)}: CubeClickHandler не найден!");
        }
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Split()
    {
        int amountParts = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < amountParts; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            Vector3 spawnPosition = transform.position + direction * (_size * _spawnOffset);
            float newSize = _size * _spawnScaleFactor;
            float newChance = _splitChance / _chanceDecreaseDivider;
            Color newColor = GenerateRandomColor();

            var newCube = CubeSpawner.Instance.Create(spawnPosition, newSize, newChance, newColor);
            newCube.GetComponent<Rigidbody>().AddForce(direction * _explotionForce, ForceMode.Impulse);
        }

        CubeSplit?.Invoke(this);
        Destroy(gameObject);
    }

    private Color GenerateRandomColor()
    {
        return Random.ColorHSV(
            _hueMin, _hueMax,
            _saturationMin, _saturationMax,
            _valueMin, _valueMax);
    }

    private void OnDisable()
    {
        var handler = CubeClickHandler.Instance;

        if (handler != null)
        {
            handler.CubeClicked -= OnCubeClicked;
        }
    }
}
