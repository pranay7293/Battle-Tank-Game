using System;
using UnityEngine;
public class EnemyController
{
    private EnemyModel EnemyModel { get; set; }
    public EnemyView EnemyView { get; set; }
    private readonly BulletService BulletService;

    private TankController TankController { get; set; }
    private readonly string type = "EnemyBullet";


    public EnemyController(EnemyModel enemyModel, EnemyView enemyView, Vector3 spawnPoint)
    {
        EnemyModel = enemyModel;
        EnemyView = GameObject.Instantiate<EnemyView>(enemyView, spawnPoint, Quaternion.identity);
        EnemyView.SetEnemyController(this);
        BulletService = EnemyView.GetBulletService();
        TankController = TankService.Instance.TankController;

    }
    public EnemyModel GetEnemyModel()
    {
        return EnemyModel;
    }
    public void Fire()
    {
        BulletService.EnemySpawnBullet(BulletService.transform, type);
    }
  
    public float Getdistance()
    {
        if (TankController != null && TankController.TankView != null)
        {
            Vector3 direction = TankController.TankView.transform.position - EnemyView.transform.position;
            float distance = direction.magnitude;
            return distance;
        }
        return 20f;
    }
    public void ApplyDamage(int damage)
    {
        if (EnemyModel.EnemyHealth - damage <= 0)
        {
            EnemyView.GetEnemyHealth().UpdateHealth(0);
            EnemyView.DestroyEnemyTank();
        }
        else
        {
            EnemyModel.EnemyHealth -= damage;
            EnemyView.GetEnemyHealth().UpdateHealth(EnemyModel.EnemyHealth);
        }
    }

    public void DestroyTank()
    {
        EnemyView.DestroyEnemyTank();
    }

    public void Enable()
    {
        EnemyView.Enabled();
    }
    public void Disable()
    {
        EnemyView.Disable();
    }
}