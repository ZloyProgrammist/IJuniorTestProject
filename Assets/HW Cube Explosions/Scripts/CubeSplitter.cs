using UnityEngine;
using Color = UnityEngine.Color;

public class CubeSplitter : MonoBehaviour
{
    [Header("Ссылки")]
    [Header("Ссылки")]
    [Header("Ссылки")]
    [Header("Ссылки")]
    [SerializeField] private CubeClickReader _clickReader;
    [SerializeField] private CubeSpawner _spawner;

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

    private void OnEnable()
    {
        if (_clickReader != null)
        {
            _clickReader.CubeClicked += OnCubeClicked;
        }
        else
        {
            Debug.LogWarning($"{nameof(CubeClickReader)}: CubeClickReader не найден!");
        }
    }

    private void OnCubeClicked(SplitableCube cube)
    {
        if (Random.value <= cube.SplitChance)
        {
            Split(cube);
        }
        else
        {
            Destroy(cube.gameObject);
        }
    }

    private void Split(SplitableCube cube)
    {
        int amountParts = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < amountParts; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            Vector3 spawnPosition = cube.transform.position + direction * (cube.Size * _spawnOffset);
            float newSize = cube.Size * _spawnScaleFactor;
            float newChance = cube.SplitChance / _chanceDecreaseDivider;
            Color newColor = GenerateRandomColor();

            var newCube = _spawner.Create(spawnPosition, newSize, newChance, newColor);
            newCube.GetComponent<Rigidbody>().AddForce(direction * _explotionForce, ForceMode.Impulse);
        }

        Destroy(cube.gameObject);
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
        if (_clickReader != null)
        {
            _clickReader.CubeClicked -= OnCubeClicked;
        }
    }
}
