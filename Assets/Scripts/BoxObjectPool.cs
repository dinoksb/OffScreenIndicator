using System.Collections.Generic;
using UnityEngine;

public class BoxObjectPool : MonoBehaviour
{
    public static BoxObjectPool Current;

    [Tooltip("Assign the box prefab.")]
    public Indicator PooledObject;
    [Tooltip("Initial pooled amount.")]
    public int PooledAmount = 1;
    [Tooltip("Should the pooled amount increase.")]
    public bool WillGrow = true;

    private List<Indicator> _pooledObjects;

    void Awake()
    {
        Current = this;
    }

    void Start()
    {
        _pooledObjects = new List<Indicator>();

        for (int i = 0; i < PooledAmount; i++)
        {
            Indicator box = Instantiate(PooledObject);
            box.transform.SetParent(transform, false);
            box.Activate(false);
            _pooledObjects.Add(box);
        }
    }

    /// <summary>
    /// Gets pooled objects from the pool.
    /// </summary>
    /// <returns></returns>
    public Indicator GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].Active)
            {
                return _pooledObjects[i];
            }
        }
        if (WillGrow)
        {
            Indicator box = Instantiate(PooledObject);
            box.transform.SetParent(transform, false);
            box.Activate(false);
            _pooledObjects.Add(box);
            return box;
        }
        return null;
    }

    /// <summary>
    /// Deactive all the objects in the pool.
    /// </summary>
    public void DeactivateAllPooledObjects()
    {
        foreach (Indicator box in _pooledObjects)
        {
            box.Activate(false);
        }
    }
}
