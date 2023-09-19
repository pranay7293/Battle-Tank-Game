using System;
using UnityEngine;

public class BulletController
{
    public BulletView BulletView { get; }
    private BulletModel BulletModel { get; }
    private readonly Rigidbody bulletRb;
    private readonly GameObject bulletObj;
    private BulletType bulletType;
    private Transform bulletSpawn;


    public BulletController(BulletView viewBullet, BulletModel modelBullet, Transform spawnTransform, BulletType bulletType)
    {
        BulletView = GameObject.Instantiate<BulletView>(viewBullet, spawnTransform.position, spawnTransform.rotation);
        BulletModel = modelBullet;
        BulletView.SetBulletController(this);
        bulletRb = BulletView.GetRigidbody();
        bulletObj = BulletView.gameObject;
        this.bulletType = bulletType;
        this.bulletSpawn = spawnTransform;
    }

    public void ShootBullet()
    {
        bulletRb.velocity = bulletSpawn.forward * BulletModel.BulletSpeed;
    }

    public BulletModel GetBulletModel()
    {
        return BulletModel;
    }
    public BulletType GetBulletType()
    {
        return bulletType;
    }

    public void Enable()
    {
        BulletView.Enable();
    }
    public void Disable()
    {
        BulletView.Disable();
    }

    public GameObject GetBulletGameObject()
    {
        return bulletObj;
    }

    public void SetInitialTransform(Transform spawn)
    {
        bulletObj.transform.position = spawn.position;
        bulletObj.transform.rotation = spawn.rotation;
    }
}