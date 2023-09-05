using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObjectList enemyTanksList;
    [SerializeField] private float range;
    [SerializeField] private int spawnCount;
    private Vector3 randomPoint;
   

    [SerializeField] public List<EnemyController> ListofEnemies { get; private set; } = new List<EnemyController>();


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
        int randomNumber = (int)Random.Range(0f, enemyTanksList.enemieTanks.Length - 1);
        EnemyScriptableObject obj = enemyTanksList.enemieTanks[randomNumber];
        EnemyModel enemyModel = new EnemyModel(obj);
        EnemyController enemyController = new EnemyController(enemyModel, obj.enemyView, spawnPoint);
        ListofEnemies.Add(enemyController);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
