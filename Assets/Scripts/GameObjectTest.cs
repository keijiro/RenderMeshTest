using UnityEngine;
using UnityEngine.Profiling;
using System.Collections.Generic;

sealed class GameObjectTest : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    PositionBuffer _buffer;
    List<Transform> _instances = new List<Transform>();

    void Start()
    {
        _buffer = new PositionBuffer(_counts.x, _counts.y);

        Profiler.BeginSample("Object Pool Init");

        for (var i = 0; i < _buffer.Positions.Length; i++)
        {
            var go = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform);
            _instances.Add(go.transform);
        }

        Profiler.EndSample();
    }

    void OnDestroy()
      => _buffer.Dispose();

    void Update()
    {
        _buffer.Update(Time.time);

        var positions = _buffer.Positions;

        Profiler.BeginSample("Mass Mesh Update");

        for (var i = 0; i < positions.Length; i++)
            _instances[i].localPosition = positions[i];

        Profiler.EndSample();
    }
}
