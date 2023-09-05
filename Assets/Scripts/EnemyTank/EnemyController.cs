using UnityEngine;
public class EnemyController
{
    private EnemyModel EnemyModel { get; set; }
    private EnemyView EnemyView { get; set; }
    private BulletService BulletService { get; set; }

    public EnemyController(EnemyModel enemyModel, EnemyView enemyView, Vector3 spawnPoint)
    {
        EnemyModel = enemyModel;
        EnemyView = GameObject.Instantiate<EnemyView>(enemyView, spawnPoint, Quaternion.identity);
        EnemyView.SetEnemyController(this);
        BulletService = EnemyView.GetBulletService();

    }
    public EnemyModel GetEnemyModel()
    {
        return EnemyModel;
    }
    public void Fire()
    {
        BulletService bulletService = EnemyView.GetBulletService();
        bulletService.SpawnBullet(bulletService.transform);
    }
    public void DestroyTank()
    {
        EnemyView.DestroyEnemyTank();
    }
}
