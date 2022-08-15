using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;

sealed class RenderMeshTest : MonoBehaviour
{
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    PositionBuffer _buffer;
    Stopwatch _stopwatch = new Stopwatch();

    void Start()
      => _buffer = new PositionBuffer(_counts.x, _counts.y);

    void OnDestroy()
      => _buffer.Dispose();

    void Update()
    {
        _buffer.Update(Time.time);

        var rparams = new RenderParams(_material);
        var matrices = _buffer.Matrices;

        _stopwatch.Reset();
        _stopwatch.Start();

        for (var i = 0; i < matrices.Length; i++)
            Graphics.RenderMesh(rparams, _mesh, 0, matrices[i]);

        _stopwatch.Stop();
        Debug.Log($"{_stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}
