using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public EnemyModel EnemyModel { get; set; }
    public EnemyController EnemyController { get; set; }
    public TankView PlayerTank1 { get => PlayerTank; set => PlayerTank = value; }

    public NavMeshAgent enemyAgent;

    private TankView PlayerTank;
    private EnemyStates currentState;
    public EnemyIdleState idleState;
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;


    [SerializeField] private BulletService bulletService;
    private int sightRange;
    private int attackRange;
    private bool playerInSightRange = false;
    private bool playerInAttackRange = false;

    private void Start()
    {
        PlayerTank = FindObjectOfType<TankView>();
        EnemyModel = EnemyController.GetEnemyModel();
        sightRange = EnemyModel.SightRange; 
        attackRange = EnemyModel.AttackRange;
        EnemyModel = EnemyController.GetEnemyModel();
        ChangeState(idleState);
        enemyAgent = GetComponent<NavMeshAgent>();
        if (PlayerTank == null)
        {
            Debug.LogError("PlayerTank not found!");
        }
    }
    void Update()
    {
        if (PlayerTank != null)
        {
            currentState.Tick();
        }
            
        playerInSightRange = Vector3.Distance(this.transform.position, PlayerTank.transform.position) <= sightRange;
        playerInAttackRange = Vector3.Distance(this.transform.position, PlayerTank.transform.position) <= attackRange;

        if (!playerInSightRange && !playerInAttackRange)
        {
            ChangeState(patrolState);
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChangeState(chaseState);
        }
        else if (playerInAttackRange && playerInSightRange)
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
        Destroy(explosion, 1.5f);
    }
 
}
