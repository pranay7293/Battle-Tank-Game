using UnityEngine;

public class EnemyModel
{
    public EnemyController EnemyController { get; set; }
    public float EnemySpeed { get; set; }
    public int EnemyHealth { get; set; }

    public float EnemyRange { get; set; }
    public float SightRange { get; set; }
    public float AttackRange { get; set; }
    public ParticleSystem EnemyExplosion { get; set; }
    public AudioClip ShootClip { get; set; }
    public int MinDealDamage { get; set; }
    public int MaxDealDamage { get; set; }

    public int DealDamage
    {
        get
        {
            return Random.Range(MinDealDamage, MaxDealDamage + 1);
        }
    }

    public EnemyModel(EnemyScriptableObject enemyScriptableObject)
    {
        EnemySpeed = enemyScriptableObject.speed;
        EnemyHealth = enemyScriptableObject.enemyHealth;
        EnemyRange = enemyScriptableObject.range;
        EnemyExplosion = enemyScriptableObject.deathExplosion;
        ShootClip = enemyScriptableObject.shootClip;
        SightRange = enemyScriptableObject.sightRange;
        AttackRange = enemyScriptableObject.attackRange;
        MinDealDamage = enemyScriptableObject.minDealDamage;
        MaxDealDamage = enemyScriptableObject.maxDealDamage;
    }   
}
