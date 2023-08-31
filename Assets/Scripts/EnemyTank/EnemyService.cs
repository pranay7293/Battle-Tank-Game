using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObjectList enemyList;
    private Vector3 randomPoint;
    public int spawnCount;
    [SerializeField] private float range;

    private void Start()
    {
        for(int i  = 0; i < spawnCount; i++)
        {
            if (RandomPoint(Vector3.zero, range, out randomPoint))
            {
                SpawnEnemy(randomPoint);
            } 
        }
    }
    
    private void SpawnEnemy(Vector3 spawnPoint)
    {
        int randomNumber = (int)Random.Range(0f, enemyList.enemies.Length - 1);
        EnemyScriptableObject obj = enemyList.enemies[randomNumber];
        EnemyModel model = new EnemyModel(obj);
        EnemyController enemyController = new EnemyController(model, obj.enemyView, spawnPoint);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
