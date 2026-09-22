using System.Collections.Generic;
using UnityEngine;

public class PoolManagerExample : MonoBehaviour
{
    [SerializeField] PooledObj[] someObjectsToPool;
    [SerializeField] int selectedObj;
    [SerializeField] private List<PoolExample> _Factories;

    private void Awake()
    {
        for (int i = 0; i < someObjectsToPool.Length; i++)
        {
            _Factories.Add(new PoolExample(someObjectsToPool[i], ReturnAction));
        }
    }

    private void ReturnAction(PooledObj pooledObj)
    {
        pooledObj.gameObject.SetActive(false);
    }

    public void Summon()
    {
        _Factories[selectedObj].Give();
    }
}
