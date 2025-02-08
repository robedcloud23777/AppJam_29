using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PooledPrefab<T> : MonoBehaviour where T : PooledPrefab<T>
{
    protected virtual int maxPoolSize => 100;
    protected virtual int defaultPoolSize => 0;

    public T prefabOrigin { get; protected set; } = null;
    Pooler<T> pool = null;
    public T Instantiate()
    {
        if (prefabOrigin != null) return prefabOrigin.Instantiate();
        if (pool == null) pool = new Pooler<T>(() =>
        {
            T tmp = Create();
            tmp.OnCreate();
            return tmp;
        }, maxPoolSize, defaultPoolSize);
        T tmp = pool.GetObject();
        tmp.prefabOrigin = this as T;
        tmp.released = false;
        return tmp;
    }
    public bool released { get; private set; } = false;
    public void Release()
    {
        if (prefabOrigin == null || released) return;
        prefabOrigin.pool.ReleaseObject(this as T);
        released = true;
        OnRelease();
    }
    protected virtual T Create()
    {
        return Instantiate((this as T));
    }
    protected virtual void OnCreate() { }
    protected virtual void OnRelease() { }
}