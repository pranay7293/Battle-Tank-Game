using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyStates
{
    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public override void Tick()
    {
        if (EnemyView.enemyAgent.remainingDistance <= EnemyView.enemyAgent.stoppingDistance) 
        {
            if (RandomPoint(transform.position, EnemyView.EnemyModel.EnemyRange, out Vector3 point)) 
            {
                EnemyView.enemyAgent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
