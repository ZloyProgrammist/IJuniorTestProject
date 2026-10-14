using System;
using UnityEngine;

public class CubeClickReader : MonoBehaviour
{
    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private Camera _camera;

    public event Action<SplitableCube> CubeClicked;

    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        if (_camera == null)
        {
            Debug.LogError($"{nameof(CubeClickReader)}: камера не найдена!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _cubeLayer))
            {
                if (hit.collider.TryGetComponent<SplitableCube>(out var cube))
                {
                    CubeClicked?.Invoke(cube);
                }
            }
        }
    }
}
