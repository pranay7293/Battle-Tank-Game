using UnityEngine;
public class EnemyController
{
    private EnemyModel EnemyModel { get; set; }
    private EnemyView EnemyView { get; set; }
    private BulletService BulletService { get; set; }

    private TankController TankController { get; set; }

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
        BulletService.SpawnBullet(BulletService.transform);
    }
    public void DestroyTank()
    {
        EnemyView.DestroyEnemyTank();
    }
    public float Getdistance()
    {
        Vector3 direction = TankController.TankView.transform.position - EnemyView.transform.position;
        float distance = direction.magnitude;
        return distance;
    }
}
