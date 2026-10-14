using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private SplitableCube _cubePrefab;

    private void Awake()
    {
        if (_cubePrefab == null)
        {
            Debug.LogError($"{nameof(CubeSpawner)}: не назначен префаб!");
        }
    }

    public SplitableCube Create(Vector3 position, float size, float splitChance, Color color)
    {
        var cube = Instantiate(_cubePrefab, position, Random.rotation);
        cube.Initialize(size, splitChance, color);
        return cube;
    }
}
