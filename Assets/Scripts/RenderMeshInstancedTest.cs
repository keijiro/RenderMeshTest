using UnityEngine;
using Unity.Collections;

sealed class RenderMeshInstancedTest : MonoBehaviour
{
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    Matrix4x4[] _matrices;

    void Start()
      => _matrices = new Matrix4x4[_counts.x * _counts.y];

    void Update()
    {
        var t = Time.time * 2;

        for (var i = 0; i < _counts.x; i++)
        {
            var x = i - _counts.x * 0.5f + 0.5f;
            for (var j = 0; j < _counts.y; j++)
            {
                var z = j - _counts.y * 0.5f + 0.5f;
                var y = Mathf.Sin(Mathf.Sqrt(x * x + z * z) * 0.4f - t);
                _matrices[j * _counts.x + i] = Matrix4x4.Translate(new Vector3(x, y, z));
            }
        }

        var rparams = new RenderParams(_material);

        for (var offs = 0; offs < _matrices.Length; offs += 1023)
        {
            var count = Mathf.Min(1023, _matrices.Length - offs);
            Graphics.RenderMeshInstanced(rparams, _mesh, 0, _matrices, count, offs);
        }
    }
}
