using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/NewEnemyScriptableObject")]
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyView enemyView;
    public float speed;
    public float range;
    public ParticleSystem deathExplosion;
    public AudioClip shootClip;
    public int sightRange;
    public int attackRange;

}
