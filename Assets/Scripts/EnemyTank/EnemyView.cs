using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    private EnemyController EnemyController { get; set; }
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(transform.position, EnemyController.GetEnemyModel().Range, out point)) 
            {
                agent.SetDestination(point);
            }
        }   
    }
   

    public void SetEnemyController(EnemyController enemyController)
    {
        EnemyController = enemyController;
    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            DestroyEnemyTank();
        }
    }

    public void DestroyEnemyTank()
    {
        ParticleSystem explosion = Instantiate(EnemyController.GetEnemyModel().Explosion, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
        Destroy(explosion, 1.5f);
    }
 
}