using System;

public interface IPooled
{
    Action<PooledObj> Returner { get; set; }
    void ReturnToPool();
}
