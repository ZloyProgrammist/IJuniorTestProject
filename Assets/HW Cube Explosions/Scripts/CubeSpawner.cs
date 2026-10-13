using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance { get; private set; }

    [SerializeField] private SplitableCube _cubePrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (_cubePrefab == null)
        {
            Debug.LogError($"{nameof(CubeSpawner)}: не назначен префаб!");
        }

        Instance = this;
    }

    public SplitableCube Create(Vector3 position, float size, float splitChance, Color color)
    {
        var cube = Instantiate(_cubePrefab, position, Random.rotation);
        cube.Initialize(size, splitChance, color);
        return cube;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
