using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Profiling;

sealed class RenderMeshInstancedTest : MonoBehaviour
{
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    PositionBuffer _buffer;

    void Start()
      => _buffer = new PositionBuffer(_counts.x, _counts.y);

    void OnDestroy()
      => _buffer.Dispose();

    void Update()
    {
        _buffer.Update(Time.time);

        var matrices = _buffer.Matrices;

        var rparams = new RenderParams(_material)
          { receiveShadows = true,
            shadowCastingMode = ShadowCastingMode.On };

        Profiler.BeginSample("Mass Mesh Update");

        for (var offs = 0; offs < matrices.Length; offs += 1023)
        {
            var count = Mathf.Min(1023, matrices.Length - offs);
            Graphics.RenderMeshInstanced(rparams, _mesh, 0, matrices, count, offs);
        }

        Profiler.EndSample();
    }
}
