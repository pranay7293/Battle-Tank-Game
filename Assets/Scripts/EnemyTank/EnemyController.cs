using UnityEngine;
using UnityEngine.AI;

public class EnemyController
{
    private EnemyModel EnemyModel { get; set; }
    private EnemyView EnemyView { get; set; }
    private NavMeshAgent navMeshAgent { get; set; }

    public EnemyController(EnemyModel enemyModel, EnemyView enemyView, Vector3 spawnPoint)
    {
        EnemyModel = enemyModel;
        EnemyView = GameObject.Instantiate<EnemyView>(enemyView, spawnPoint, Quaternion.identity);
        EnemyView.SetEnemyController(this);
    }
    public EnemyModel GetEnemyModel()
    {
        return EnemyModel;
    }
}
