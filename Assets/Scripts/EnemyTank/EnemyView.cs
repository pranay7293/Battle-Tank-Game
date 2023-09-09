using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public EnemyModel EnemyModel { get; set; }
    public EnemyController EnemyController { get; set; }
    public NavMeshAgent enemyAgent;

    private EnemyStates currentState;
    public EnemyIdleState idleState;
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;

    private bool inSightRange = false;
    private bool inAttackRange = false;


    [SerializeField] private BulletService bulletService;

    private void Start()
    {
        
        EnemyModel = EnemyController.GetEnemyModel();
        ChangeState(idleState);
        enemyAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (TankService.Instance.TankController.TankView != null)
            currentState.Tick();
        inSightRange = (EnemyController.Getdistance() <= EnemyModel.SightRange);
        inAttackRange = (EnemyController.Getdistance() <= EnemyModel.AttackRange);

        if(!inSightRange && !inAttackRange)
        {
            ChangeState(patrolState);
        }
        else if (inSightRange && !inAttackRange)
        {
            ChangeState(chaseState);
        }
        else if (inSightRange && inAttackRange)
        {
            ChangeState(attackState);
        }
    }
    
    public void ChangeState(EnemyStates newState)
    {
        if (currentState != null)
        {
            currentState.OnExitState();
        }

        currentState = newState;
        currentState.OnEnterState();
    }
    
    public AudioSource GetAudioSource()
    {
        return gameObject.GetComponent<AudioSource>();
    }

    public BulletService GetBulletService()
    {
        return bulletService;
    }

    public void SetEnemyController(EnemyController enemyCtrl)
    {
        EnemyController = enemyCtrl;
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
        ParticleSystem explosion = Instantiate(EnemyController.GetEnemyModel().EnemyExplosion, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
        Destroy(explosion.gameObject, 1.5f);
    }
 
}
