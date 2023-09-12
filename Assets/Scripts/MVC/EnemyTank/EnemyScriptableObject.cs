using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/NewEnemyScriptableObject")]
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyView enemyView;
    public int enemyHealth;
    public float speed;
    public float range;
    public ParticleSystem deathExplosion;
    public AudioClip shootClip;
    public float sightRange;
    public float attackRange;
    public int minDealDamage;
    public int maxDealDamage;

}
