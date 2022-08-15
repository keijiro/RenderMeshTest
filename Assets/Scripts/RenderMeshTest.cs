using UnityEngine;

sealed class RenderMeshTest : MonoBehaviour
{
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    void Update()
    {
        var rparams = new RenderParams(_material);
        var t = Time.time * 2;
        for (var i = 0; i < _counts.x; i++)
        {
            var x = i - _counts.x * 0.5f + 0.5f;
            for (var j = 0; j < _counts.y; j++)
            {
                var z = j - _counts.y * 0.5f + 0.5f;
                var y = Mathf.Sin(Mathf.Sqrt(x * x + z * z) * 0.4f - t);
                var pos = new Vector3(x, y, z);
                var matrix = Matrix4x4.Translate(pos);
                Graphics.RenderMesh(rparams, _mesh, 0, matrix);
            }
        }
    }
}
