using UnityEngine;
using System.Collections.Generic;

sealed class ArrayOfGameObject : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Vector2Int _counts = new Vector2Int(10, 10);

    List<Transform> _instances = new List<Transform>();

    void Start()
    {
        for (var i = 0; i < _counts.x; i++)
        {
            var x = i - _counts.x * 0.5f + 0.5f;
            for (var j = 0; j < _counts.y; j++)
            {
                var pos = new Vector3(x, 0, j - _counts.y * 0.5f + 0.5f);
                var go = Instantiate(_prefab, pos, Quaternion.identity, transform);
                _instances.Add(go.transform);
            }
        }
    }

    void Update()
    {
        var t = Time.time * 2;
        foreach (var i in _instances)
        {
            var p = Vector3.Scale(i.localPosition, new Vector3(1, 0, 1));
            var y = Mathf.Sin(p.magnitude * 0.4f - t);
            i.localPosition = new Vector3(p.x, y, p.z);
        }
    }
}
