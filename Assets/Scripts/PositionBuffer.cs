using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using System;

sealed class PositionBuffer : IDisposable
{
    public NativeArray<Vector3> Positions => _arrays.p.Reinterpret<Vector3>();
    public NativeArray<Matrix4x4> Matrices => _arrays.m.Reinterpret<Matrix4x4>();

    (NativeArray<float3> p, NativeArray<float4x4> m) _arrays;
    (int x, int y) _dims;

    public PositionBuffer(int xCount, int yCount)
    {
        _dims = (xCount, yCount);
        _arrays = (new NativeArray<float3>(_dims.x * _dims.y, Allocator.Persistent),
                   new NativeArray<float4x4>(_dims.x * _dims.y, Allocator.Persistent));
    }

    public void Dispose()
    {
        if (_arrays.p.IsCreated) _arrays.p.Dispose();
        if (_arrays.m.IsCreated) _arrays.m.Dispose();
    }

    public void Update(float time)
    {
        var t = time * 2;
        var offs = 0;
        for (var i = 0; i < _dims.x; i++)
        {
            var x = i - _dims.x * 0.5f + 0.5f;
            for (var j = 0; j < _dims.y; j++)
            {
                var z = j - _dims.y * 0.5f + 0.5f;
                var y = math.sin(math.sqrt(x * x + z * z) * 0.4f - t);
                var p = math.float3(x, y, z);
                _arrays.p[offs] = p;
                _arrays.m[offs] = float4x4.Translate(p);
                offs++;
            }
        }
    }
}
