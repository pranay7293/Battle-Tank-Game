using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> bulletsFiredObservers = new List<IObserver>();
    private List<IObserver> damageObservers = new List<IObserver>();
    private List<IObserver> killsObservers = new List<IObserver>();

    public void AddBulletsFiredObservers(IObserver observer)
    {
        bulletsFiredObservers.Add(observer);
    }
    public void RemoveBulletsFiredObservers(IObserver observer)
    {
        bulletsFiredObservers.Remove(observer);
    }
    public void AddDamageObservers(IObserver observer)
    {
        damageObservers.Add(observer);
    }
    public void RemoveDamageObservers(IObserver observer)
    {
        damageObservers.Remove(observer);
    }
    public void AddKillsObservers(IObserver observer)
    {
        killsObservers.Add(observer);
    }
    public void RemoveKillsObservers(IObserver observer)
    {
        killsObservers.Remove(observer);
    }
    protected void NotifyBulletsFiredObservers()
    {
        foreach (var observer in bulletsFiredObservers)
        {
            observer.OnBulletsFired();
        }
    }

    protected void NotifyDamageObservers(int damage)
    {
        foreach (var observer in damageObservers)
        {
            observer.OnDamage(damage);
        }
    }

    protected void NotifyKillsObservers()
    {
        foreach (var observer in killsObservers)
        {
            observer.OnKills();
        }
    }

}
