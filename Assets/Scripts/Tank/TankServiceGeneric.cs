using System.Collections;
using UnityEngine;

public class TankServiceGeneric<T> : MonoBehaviour where T : TankServiceGeneric<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Debug.LogError("someone trying to create a duplicate Singleton");
            Destroy(this);
        }
    }
}
