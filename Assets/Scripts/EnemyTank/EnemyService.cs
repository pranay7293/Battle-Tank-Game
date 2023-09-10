using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyService : GenericSingleton<EnemyService>
{
    [SerializeField] private EnemyScriptableObjectList enemyTanksList;
    [SerializeField] private float range;
    [SerializeField] private int spawnCount;
    private Vector3 randomPoint;
    [SerializeField] private AchievementSystem achievementSystem;

    private EnemyController enemyController;

    public EnemyController EnemyController => enemyController;
    [SerializeField] public List<EnemyController> ListofEnemies { get;  set; } = new List<EnemyController>();


    private void Start()
    {
        for(int i  = 0; i < spawnCount; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (RandomPoint(Vector3.zero, range, out randomPoint) && spawnCount > 0)
                    break;
            }
            SpawnEnemy(randomPoint);
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        int randomNumber = (int)Random.Range(0f, enemyTanksList.enemieTanks.Length);
        EnemyScriptableObject obj = enemyTanksList.enemieTanks[randomNumber];
        EnemyModel enemyModel = new EnemyModel(obj);
        enemyController = new EnemyController(enemyModel, obj.enemyView, spawnPoint);
        ListofEnemies.Add(enemyController);
        enemyController.EnemyView.AddDamageObservers(achievementSystem);
        enemyController.EnemyView.AddKillsObservers(achievementSystem);

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
