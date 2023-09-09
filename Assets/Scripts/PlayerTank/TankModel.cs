using UnityEngine;

public class TankModel 
{
    public float TankSpeed { get; private set; }
    public int TankHealth { get; private set; }
    public int TankDamage { get; private set; }
    public ParticleSystem Explosion { get; }

    public TankTypes TankType { get;  }
    public AudioClip shootClip;

    public TankModel(TankScriptableObject tankScriptableObject) 
    {
        TankSpeed = tankScriptableObject.speed;
        TankHealth = tankScriptableObject.health;
        TankDamage = tankScriptableObject.damage;
        TankType = tankScriptableObject.tanktype;
        shootClip = tankScriptableObject.shootClip;
        Explosion = tankScriptableObject.explosion;

    }

    public float GetSpeed()
    {
        return TankSpeed;
    }
}
