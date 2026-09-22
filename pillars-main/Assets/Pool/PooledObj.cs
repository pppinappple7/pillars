using UnityEngine;
using System;

public class PooledObj : MonoBehaviour, IPooled
{
    public Action<PooledObj> Returner { get; set; }

    public void ReturnToPool()
    {
        Returner?.Invoke(this);
    }
}
