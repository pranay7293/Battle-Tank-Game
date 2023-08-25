using UnityEngine;

public class TankModel 
{
    public int Speed;
    public float Health;

    private TankController tankController;
    public TankModel(int speed, float health) 
    {
        Speed = speed;
        Health = health;
    }   

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
