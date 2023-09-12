using UnityEngine;

public class BulletService : MonoBehaviour
{
    [SerializeField] private BulletScriptableObjectList bulletList;
    public BulletController BulletController { get; set; }

    public BulletPool playerBulletPool;
    public BulletPool enemyBulletPool;
    
    private void Start()
    {
        playerBulletPool = TankService.Instance.TankController.TankView.GetBulletService().GetComponent<BulletPool>();
        enemyBulletPool = EnemyService.Instance.EnemyController.EnemyView.GetBulletService().GetComponent<BulletPool>();
    }
    public BulletController PlayerSpawnBullet(Transform spawn, string tag)
    {
        BulletScriptableObject obj = bulletList.bullets[0];
        BulletModel bulletModel = new BulletModel(obj);
        BulletController = playerBulletPool.GetBullet(obj.bulletView, bulletModel, spawn);
        BulletController.GetBulletGameObject().tag = tag;

        BulletController.ShootBullet();
        BulletController.SetTransform(spawn);
        BulletController.Enable();

        return BulletController;
    }
    public BulletController EnemySpawnBullet(Transform spawn, string tag)
    {
        BulletScriptableObject obj = bulletList.bullets[0];
        BulletModel bulletModel = new BulletModel(obj);
        BulletController = enemyBulletPool.GetBullet(obj.bulletView, bulletModel, spawn);
        BulletController.GetBulletGameObject().tag = tag;

        BulletController.ShootBullet();
        BulletController.SetTransform(spawn);
        BulletController.Enable();

        return BulletController;
    }
}
