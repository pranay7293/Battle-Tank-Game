using UnityEngine;

public class BulletController
{
    private BulletView BulletView { get; }
    private BulletModel BulletModel { get; }
    private readonly Rigidbody bulletRb;
    private readonly Transform bulletSpawn;
    private readonly GameObject bulletGameObject;
    private BulletType bulletType; 



    public BulletController(BulletView viewBullet, BulletModel modelBullet, Transform spawnTransform, BulletType bulletType)
    {
        bulletSpawn = spawnTransform;
        BulletView = GameObject.Instantiate<BulletView>(viewBullet, bulletSpawn.position, bulletSpawn.rotation);
        BulletModel = modelBullet;
        BulletView.SetBulletController(this);
        bulletRb = BulletView.GetRigidbody();
        bulletGameObject = BulletView.gameObject;
        this.bulletType = bulletType;
    }
    public void ShootBullet()
    {
        bulletRb.velocity = bulletSpawn.forward * BulletModel.BulletSpeed;
    }
    public BulletType GetBulletType()
    {
        return bulletType;
    }
    public BulletModel GetBulletModel()
    {
        return BulletModel;
    }
    public GameObject GetBulletGameObject()
    {
        return bulletGameObject;
    }
}
