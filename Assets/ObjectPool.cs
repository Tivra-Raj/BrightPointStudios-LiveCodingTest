using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour
{
    public List<PooledItem<T>> Itempool;

    private void Start()
    {
        Itempool = new List<PooledItem<T>>();
    }

    protected virtual T CreateItem()
    {
        throw new NotImplementedException("CreateItem() method not implemented in derived class");
    }

    private T CreateNewPoolItem()
    {
        PooledItem<T> newItem = new PooledItem<T>();
        newItem.Item = CreateItem();
        newItem.isActive = true;
        Itempool.Add(newItem);
        return newItem.Item;
    }

    public virtual T GetItemFromPool()
    {
        if (Itempool.Count > 0)
        {
            PooledItem<T> item = Itempool.Find(item => !item.isActive);
            if (item != null)
            {
                item.isActive = true;
                return item.Item;
            }
        }
        return CreateNewPoolItem();
    }

    public virtual void ReturnItemToPool(T item)
    {
        PooledItem<T> pooledItem = Itempool.Find(i => i.Item.Equals(item));
        if (pooledItem != null)
        {
            pooledItem.isActive = false;
        }
    }

    public class PooledItem<T>
    {
        public T Item;
        public bool isActive;
    }
}