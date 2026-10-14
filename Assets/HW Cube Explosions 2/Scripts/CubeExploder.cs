using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _referenceSize = 1;
    [SerializeField] private float _minSize = 0.05f;

    public void Explode(SplitableCube cube, float force, float radius)
    {
        float size = Mathf.Max(cube.Size, _minSize);
        float scale = _referenceSize / size;
        float explosionForce = radius * scale;
        float explosionRadius = force * scale;
        Vector3 explosionPosition = cube.transform.position;

        foreach (Rigidbody explodableObject in GetExplodableObjects(explosionPosition, explosionRadius))
        {
            explodableObject.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
