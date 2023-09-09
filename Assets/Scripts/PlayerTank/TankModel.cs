using UnityEngine;

public class TankModel 
{
    public float TankSpeed { get; private set; }
    public int TankHealth { get; set; }
    public int TankDamage { get; private set; }
    public ParticleSystem Explosion { get; }

    public TankTypes TankType { get;  }

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

    public TankModel(TankScriptableObject tankScriptableObject) 
    {
        TankSpeed = tankScriptableObject.speed;
        TankHealth = tankScriptableObject.health;
        TankType = tankScriptableObject.tanktype;
        ShootClip = tankScriptableObject.shootClip;
        Explosion = tankScriptableObject.explosion;
        MinDealDamage = tankScriptableObject.minDealDamage;
        MaxDealDamage = tankScriptableObject.maxDealDamage;

    }

    public float GetSpeed()
    {
        return TankSpeed;
    }
}
