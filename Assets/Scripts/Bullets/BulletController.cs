using UnityEngine;

public class BulletController
{
    private BulletView BulletView { get; }
    private BulletModel BulletModel { get; }
    private readonly Rigidbody bulletRb;
    private readonly Transform bulletSpawn;

    public BulletController(BulletView viewBullet, BulletModel modelBullet, Transform spawnTransform)
    {
        bulletSpawn = spawnTransform;
        BulletView = GameObject.Instantiate<BulletView>(viewBullet, bulletSpawn.position, bulletSpawn.rotation);
        BulletModel = modelBullet;
        BulletView.SetBulletController(this);
        bulletRb = BulletView.GetRigidbody();
    }
    public void ShootBullet()
    {
        bulletRb.velocity = bulletSpawn.forward * BulletModel.BulletSpeed;
    }
    public BulletModel GetBulletModel()
    {
        return BulletModel;
    }
}
