using UnityEngine;

public class EnemyModel
{
    public EnemyController EnemyController { get; set; }
    public float EnemySpeed { get; set; }
    public float EnemyRange { get; set; }
    public float SightRange { get; set; }
    public float AttackRange { get; set; }



    public ParticleSystem EnemyExplosion { get; set; }
    public AudioClip ShootClip { get; set; }


    public EnemyModel(EnemyScriptableObject enemyScriptableObject)
    {
        EnemySpeed = enemyScriptableObject.speed;
        EnemyRange = enemyScriptableObject.range;
        EnemyExplosion = enemyScriptableObject.deathExplosion;
        ShootClip = enemyScriptableObject.shootClip;
        SightRange = enemyScriptableObject.sightRange;
        AttackRange = enemyScriptableObject.attackRange;
    }   
}
