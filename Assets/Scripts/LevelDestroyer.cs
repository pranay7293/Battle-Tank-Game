using System.Collections;
using UnityEngine;

public class LevelDestroyer : GenericSingleton<LevelDestroyer>
{
    [SerializeField] private GameObject Level;
    public bool IsDead { get; set; } = false;
    private bool isRunning = false;


    void Start()
    {
        EventManager.OnTankDestroyed += OnTankDestroyed;

        if (IsDead && !isRunning)
        {
            StartCoroutine(DestroyAll());
        }
    }
    private void OnTankDestroyed()
    {
        IsDead = true;

        if (!isRunning)
        {
            StartCoroutine(DestroyAll());
        }
    }


    public IEnumerator DestroyAll()
    {
        isRunning = true;
        StartCoroutine(DestroyEnemies(2));
        StartCoroutine(DestroyGround(3));
        yield return null;
      
    }

    IEnumerator DestroyGround(int sec)
    {
        yield return new WaitForSeconds(sec);
        Level.SetActive(false);
    }
    IEnumerator DestroyEnemies(int sec)
    {
        yield return new WaitForSeconds(sec);
        for (int i = 0; i < EnemyService.Instance.ListofEnemies.Count; i++)
        {
            if (EnemyService.Instance.ListofEnemies[i] != null)
            {
                EnemyService.Instance.ListofEnemies[i].DestroyTank();
            }
        }
    }
}
