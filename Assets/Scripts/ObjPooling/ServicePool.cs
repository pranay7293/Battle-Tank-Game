using System.Collections.Generic;
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
            return pooledItem.Item;
        }
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
        }

    }
    protected class PooledItems<KT>
    {
        public KT Item;
        public bool IsUsed;
    }

}