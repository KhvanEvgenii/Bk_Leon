using UnityEngine;

enum BackGroundType
{
    Blue, Brown, Gray, Green, Pink, Purple, Yellow
}

public class Background : MonoBehaviour
{
    MeshRenderer mesh;
    [SerializeField] Vector2 movingDirection;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (mesh != null)
        {
            mesh.material.mainTextureOffset += movingDirection * Time.deltaTime;
        }
    }
}
