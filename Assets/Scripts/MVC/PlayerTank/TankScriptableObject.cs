using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/NewTankScriptableObjects")]
public class TankScriptableObject : ScriptableObject
{
    public TankTypes tanktype;
    public string tankName;
    public int health;
    public float speed;
    public TankView tankView;
    public ParticleSystem explosion;
    public int minDealDamage;
    public int maxDealDamage;

}