using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class ServicePool<T> : MonoBehaviour where T : class
{
    protected List<PooledItems<T>> pooledItems = new List<PooledItems<T>>();

    public virtual T GetItem()
    {
        PooledItems<T> item = pooledItems.Find(i => !i.IsUsed);

        if (item != null)
        {
            item.IsUsed = true;
            return item.Item;
        }

        return CreateNewPooledItem();
    }

    private T CreateNewPooledItem()
    {
        T newItem = CreateItem();

        if (newItem != null)
        {
            PooledItems<T> pooledItem = new PooledItems<T>
            {
                Item = newItem,
                IsUsed = true
            };

            pooledItems.Add(pooledItem);
            Debug.Log("New item added to pool. Total items: " + pooledItems.Count);
            return pooledItem.Item;
        }

        Debug.LogError("Failed to create a new item for the pool.");
        return null;
    }

    protected virtual T CreateItem()
    {
        return (T)null;
    }

    public virtual void ReturnItem(T item)
    {
        PooledItems<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
        if (pooledItem != null)
        {
            pooledItem.IsUsed = false;
            Debug.Log("Item returned to the pool." + pooledItem);
        }
        else
        {
            Debug.LogError("Tried to return an item not managed by this pool.");
        }
    }
    protected class PooledItems<KT>
    {
        public KT Item;
        public bool IsUsed;
    }

}