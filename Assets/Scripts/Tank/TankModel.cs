using UnityEngine;

public class TankModel 
{
    public float speed { get; private set; }
    public int health { get; private set; }
    public int damage { get; private set; }
    public TankTypes type { get;  }
    public AudioClip shootClip;

    private TankController tankController;
    public TankModel(TankScriptableObject tankScriptableObject) 
    {
        speed = tankScriptableObject.speed;
        health = tankScriptableObject.health;
        damage = tankScriptableObject.damage;
        type = tankScriptableObject.type;
        shootClip = tankScriptableObject.shootClip;

    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
