using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action OnTankDestroyed;
    public static void TankDestroyed()
    {
        OnTankDestroyed?.Invoke();
    }
}
