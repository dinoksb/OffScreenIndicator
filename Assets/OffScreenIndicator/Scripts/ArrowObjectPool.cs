using System.Collections.Generic;
using UnityEngine;

class ArrowObjectPool : MonoBehaviour
{
    public static ArrowObjectPool Current;

    [Tooltip("Assign the arrow prefab.")]
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
            Indicator arrow = Instantiate(PooledObject);
            arrow.transform.SetParent(transform, false);
            arrow.Activate(false);
            _pooledObjects.Add(arrow);
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
            Indicator arrow = Instantiate(PooledObject);
            arrow.transform.SetParent(transform, false);
            arrow.Activate(false);
            _pooledObjects.Add(arrow);
            return arrow;
        }
        return null;
    }

    /// <summary>
    /// Deactive all the objects in the pool.
    /// </summary>
    public void DeactivateAllPooledObjects()
    {
        foreach (Indicator arrow in _pooledObjects)
        {
            arrow.Activate(false);
        }
    }
}
