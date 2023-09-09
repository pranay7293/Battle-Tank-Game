using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour, IDamagable
{
    public EnemyModel EnemyModel { get; set; }
    public EnemyController EnemyController { get; set; }
    public NavMeshAgent enemyAgent;
    public EnemyHealth enemyHealth;

    private EnemyStates currentState;
    public EnemyIdleState idleState;
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;

    private int dealDamage;


    private bool inSightRange = false;
    private bool inAttackRange = false;
    public int DamageAmount { get; set; }


    [SerializeField] private BulletService bulletService;

    private void Start()
    {
        
        EnemyModel = EnemyController.GetEnemyModel();
        ChangeState(idleState);
        enemyAgent = GetComponent<NavMeshAgent>();
        dealDamage = EnemyModel.DealDamage;
    }
    void Update()
    {
        if (TankService.Instance.TankController.TankView != null)
            currentState.Tick();
        inSightRange = (EnemyController.Getdistance() <= EnemyModel.SightRange);
        inAttackRange = (EnemyController.Getdistance() <= EnemyModel.AttackRange);

        if (inSightRange && !inAttackRange)
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

    public EnemyHealth GetEnemyHealth()
    {
        return enemyHealth;
    }
    public void SetEnemyController(EnemyController enemyCtrl)
    {
        EnemyController = enemyCtrl;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.TryGetComponent<BulletView>(out var bullet))
            {
                BulletType bulletType = bullet.GetBulletController().GetBulletType();
                if (bulletType == BulletType.PlayerBullet)
                {
                    TakeDamage(dealDamage);
                }
            }
        }
    }

    public void DestroyEnemyTank()
    {
        ParticleSystem explosion = Instantiate(EnemyController.GetEnemyModel().EnemyExplosion, gameObject.transform.position, Quaternion.identity);
        explosion.Play();
        Destroy(gameObject);
        Destroy(explosion.gameObject, 1.5f);
    }

    public void TakeDamage(int damage)
    {
        EnemyController.ApplyDamage(damage);   
    }
}

