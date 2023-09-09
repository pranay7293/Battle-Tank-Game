using System.Collections;
using UnityEngine;

public class LevelDestroyer : GenericSingleton<LevelDestroyer>
{
    private EnemyView[] enemies;
    [SerializeField] private GameObject Level;
    public bool IsDead { get; set; } = false;
    private bool isRunning = false;


    void Start()
    {
        if (IsDead && !isRunning)
        {
            StartCoroutine(DestroyAll());
        }
    }

    
    public IEnumerator DestroyAll()
    {
        isRunning = true;
        enemies = FindObjectsOfType<EnemyView>();        
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
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                enemies[i].GetComponent<EnemyView>().DestroyEnemyTank();
        }
    }
}
