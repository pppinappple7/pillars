using System;
using UnityEngine;

public class PoolExample : Pool
{
    private PooledObj _prefab;

    public PoolExample(PooledObj prefab, Action<PooledObj> returner) : base(returner)
    {
        _prefab = prefab;
    }

    public override PooledObj Give()
    {
        if (_items.Count > 0)
        {
            var pooledObj = _items[_items.Count - 1];
            pooledObj.gameObject.SetActive(true);
            _items.RemoveAt(_items.Count - 1);

            return pooledObj;
        }
        else 
        {
            var cratedObj = GameObject.Instantiate(_prefab);
            cratedObj.gameObject.SetActive(true);
            cratedObj.Returner += ReturnToList;
            cratedObj.Returner += _returner; 

            return cratedObj;
        }
    }
    public void ReturnToList(PooledObj pooled)
    {
        _items.Add(pooled);
    }
}
