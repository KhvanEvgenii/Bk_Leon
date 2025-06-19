using UnityEngine;

public class SizeChecker : MonoBehaviour
{
    [SerializeField] Vector3 size;
    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        size = meshRenderer.bounds.size;
    }
}
