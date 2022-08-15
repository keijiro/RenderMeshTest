using UnityEngine;
using System.Collections.Generic;
using Stopwatch = System.Diagnostics.Stopwatch;

sealed class GameObjectTest : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    PositionBuffer _buffer;
    List<Transform> _instances = new List<Transform>();
    Stopwatch _stopwatch = new Stopwatch();

    void Start()
    {
        _buffer = new PositionBuffer(_counts.x, _counts.y);

        for (var i = 0; i < _buffer.Positions.Length; i++)
        {
            var go = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform);
            _instances.Add(go.transform);
        }
    }

    void OnDestroy()
      => _buffer.Dispose();

    void Update()
    {
        _buffer.Update(Time.time);

        var positions = _buffer.Positions;

        _stopwatch.Reset();
        _stopwatch.Start();

        for (var i = 0; i < positions.Length; i++)
            _instances[i].localPosition = positions[i];

        _stopwatch.Stop();
        Debug.Log($"{_stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}
