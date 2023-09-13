using UnityEngine;

public class BulletService : MonoBehaviour
{
    [SerializeField] private BulletScriptableObjectList bulletList;
    public BulletController BulletController { get; set; }

    private BulletPool playerBulletPool;
    private BulletPool enemyBulletPool;
    
    private void Start()
    {
        playerBulletPool = GetComponent<BulletPool>();
        enemyBulletPool = GetComponent<BulletPool>();
    }
    public BulletController SpawnBullet(Transform spawn, BulletType bulletType)
    {
        BulletScriptableObject obj = bulletList.bullets[0];
        BulletModel bulletModel = new BulletModel(obj);
        if(bulletType == BulletType.PlayerBullet)
        {
            BulletController = playerBulletPool.GetBullet(obj.bulletView, bulletModel, spawn, bulletType);
        }
        else if(bulletType == BulletType.EnemyBullet)
        {
            BulletController = enemyBulletPool.GetBullet(obj.bulletView, bulletModel, spawn, bulletType);
        }
       
        BulletController.SetTransform(spawn);
        BulletController.ShootBullet();
        BulletController.Enable();

        return BulletController;
    }
    
}
