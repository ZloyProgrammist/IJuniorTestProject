using System;
using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    public static CubeClickHandler Instance { get; private set; }

    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private Camera _camera;

    public event Action<SplitableCube> CubeClicked;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (_camera == null)
        {
            _camera = Camera.main;
        }

        if (_camera == null)
        {
            Debug.LogError($"{nameof(CubeClickHandler)}: камера не найдена!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit,_maxDistance, _cubeLayer))
            {
                if (hit.collider.TryGetComponent<SplitableCube>(out var cube))
                {
                    CubeClicked?.Invoke(cube);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
