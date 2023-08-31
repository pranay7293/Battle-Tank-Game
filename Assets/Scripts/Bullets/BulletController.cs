using UnityEngine;

public class BulletController
{
    private BulletView BulletView { get; }
    private BulletModel BulletModel { get; }
    private Rigidbody bulletRb;
    private Transform bulletSpawn;

    public BulletController(BulletView bulletView, BulletModel bulletModel, Transform spawnTransform)
    {
        bulletSpawn = spawnTransform; 
        BulletView = GameObject.Instantiate<BulletView>(bulletView, bulletSpawn.position, bulletSpawn.rotation);
        BulletModel = bulletModel;
        BulletView.SetBulletController(this);
        bulletRb = BulletView.GetRigidbody();
    }
    public void ShootBullet()
    {
        bulletRb.velocity = bulletSpawn.forward * BulletModel.Speed;
    }
    public BulletModel GetBulletModel()
    {
        return BulletModel;
    }
}
