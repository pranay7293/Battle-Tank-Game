using UnityEngine;

public class BulletService : MonoBehaviour
{
    [SerializeField] private BulletScriptableObjectList bulletList;
    public BulletController BulletController { get; set; }

    private BulletPool playerBulletPool;
    private BulletPool enemyBulletPool;

    private void Start()
    {
        playerBulletPool = TankService.Instance.TankController.TankView.GetBulletService().GetComponent<BulletPool>();
        enemyBulletPool = EnemyService.Instance.EnemyController.EnemyView.GetBulletService().GetComponent<BulletPool>();
    }
    public BulletController SpawnBullet(Transform spawn, BulletType bulletType)
    {       
        if (bulletType == BulletType.PlayerBullet)
        {
            BulletScriptableObject obj = bulletList.bullets[0];
            BulletModel bulletModel = new BulletModel(obj);
            BulletController = playerBulletPool.GetBullet(obj.bulletView, bulletModel, spawn, bulletType);
            BulletController.Enable();
            BulletController.SetInitialTransform(spawn); // Set the initial position and rotation
            BulletController.ShootBullet();
            return BulletController;
        }
        else if (bulletType == BulletType.EnemyBullet)
        {
            BulletScriptableObject obj = bulletList.bullets[0];
            BulletModel bulletModel = new BulletModel(obj);
            BulletController = enemyBulletPool.GetBullet(obj.bulletView, bulletModel, spawn, bulletType);
            BulletController.Enable();
            BulletController.SetInitialTransform(spawn); // Set the initial position and rotation
            BulletController.ShootBullet();
            return BulletController;
        }
        return null;
    }

}