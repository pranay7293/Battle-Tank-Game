using UnityEngine;

public class BulletPool : ServicePool<BulletController>
{
    private BulletView _bulletView;
    private BulletModel _bulletModel;
    private Transform _spawnPoint;

    public BulletController GetBullet(BulletView viewBullet, BulletModel modelBullet, Transform spawnTransform)
    {
        this._bulletView = viewBullet;
        this._bulletModel = modelBullet;
        this._spawnPoint = spawnTransform;
        return GetItem();

    }
    protected override BulletController CreateItem()
    {
        BulletController bulletController = new BulletController(_bulletView, _bulletModel, _spawnPoint);

        return bulletController;
    }
    public override void ReturnItem(BulletController bulletController)
    {
        PooledItems<BulletController> pooledItem = pooledItems.Find(i => i.Item.Equals(bulletController));
        if (pooledItem != null)
        {
            pooledItem.IsUsed = false;
            bulletController.Disable();
        }
    }
}
