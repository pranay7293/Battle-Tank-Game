using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : Subject, IDamagable
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
    public BulletService bulletService;
    public Transform bulletSpawn;

    private int dealDamage;
    private bool inSightRange = false;
    private bool inAttackRange = false;


    [SerializeField] 

    private void Start()
    {        
        EnemyModel = EnemyController.GetEnemyModel();
        ChangeState(idleState);
        enemyAgent = GetComponent<NavMeshAgent>();
        dealDamage = EnemyModel.DealDamage;
    }
    void Update()
    {
        if (TankService.Instance.TankController != null && TankService.Instance.TankController.TankView != null)
        {
            inSightRange = (EnemyController.Getdistance() <= EnemyModel.SightRange);
            inAttackRange = (EnemyController.Getdistance() <= EnemyModel.AttackRange);

            if (inSightRange && !inAttackRange)
            {
                ChangeState(chaseState);
            }
            else if (inAttackRange)
            {
                ChangeState(attackState);
            }
            currentState.Tick();
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

    public Transform GetBulletSpawnTransform() 
    { return bulletSpawn; }

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
        BulletView bulletView = collision.gameObject.GetComponent<BulletView>();

        if (bulletView != null)
        {
            BulletController bulletController = bulletView.GetBulletController();

            if (bulletController != null)
            {
                BulletType bulletType = bulletController.GetBulletType();

                if (bulletType == BulletType.PlayerBullet)
                {
                    TakeDamage(dealDamage);
                    NotifyDamageObservers(dealDamage);
                }
            }
        }
    }

    public void DestroyEnemyTank()
    {
        NotifyKillsObservers();
        DestroyEnemy();
        TankService.Instance.TankController.TankView.UpdateEnemiesCount();
    }
    public void DestroyEnemy()
    {
        ParticleSystem explosion = Instantiate(EnemyController.GetEnemyModel().EnemyExplosion, gameObject.transform.position, Quaternion.identity);
        explosion.Play();
        GameObject bustedTank = Instantiate(EnemyController.GetEnemyModel().BustedTank, gameObject.transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(Sounds.TankExplosion);
        DisableEnemy();
        EnemyService.Instance.ListofEnemies.Remove(EnemyController);
        Destroy(explosion.gameObject, 1.5f);
        Destroy(bustedTank, 4f);
    }

    private void DisableEnemy()
    {
        Disable();
        EnemyService.Instance.enemyTankPool.ReturnItem(EnemyController);
    }

    public void TakeDamage(int damage)
    {
        EnemyController.ApplyDamage(damage);        
    }

    public void Enabled()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    
}

