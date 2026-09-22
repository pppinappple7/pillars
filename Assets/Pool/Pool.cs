using System;
using System.Collections.Generic;

public abstract class Pool
{
    protected List<PooledObj> _items;
    protected Action<PooledObj> _returner;

    public Pool(Action<PooledObj> returner)
    {
        _items = new List<PooledObj>();
        _returner = returner;
    }

    public abstract PooledObj Give();
}
