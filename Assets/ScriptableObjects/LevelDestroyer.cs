using System.Collections;
using UnityEngine;

public class LevelDestroyer : MonoBehaviour
{
    private EnemyView[] enemies;
    private TankView player;
    [SerializeField] private GameObject Level;

    void Start()
    {
        this.gameObject.SetActive(true);
    }

    
    public IEnumerator DestroyAll()
    {
        enemies = FindObjectsOfType<EnemyView>();
        player = FindObjectOfType<TankView>();
        yield return new WaitForSeconds(1);
        Debug.Log("DestroyAll Coroutine Started");
        yield return StartCoroutine(DestroyPlayer(1));
        Debug.Log("DestroyPlayer Coroutine Finished");
        yield return StartCoroutine(DestroyEnemies(2));
        Debug.Log("DestroyEnemies Coroutine Finished");
        yield return StartCoroutine(DestroyGround(3));
      
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

   
    IEnumerator DestroyPlayer(int sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(player);
    }
}
