using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyTankPool : ServicePool <EnemyController>
{
    private EnemyModel _enemyModel;
    private EnemyView _enemyView;
    private Vector3 _spawnPoint;

    public EnemyController GetEnemyTank(EnemyModel enemyModel, EnemyView enemyView, Vector3 spawnPoint)
    {
        this._enemyModel = enemyModel;
        this._enemyView = enemyView;
        this._spawnPoint = spawnPoint;
        return GetItem();

    }
    protected override EnemyController CreateItem()
    {
        EnemyController enemyController = new EnemyController(_enemyModel, _enemyView, _spawnPoint);

        return enemyController;
    }

    public override void ReturnItem(EnemyController enemyController)
    {
        PooledItems<EnemyController> pooledItem = pooledItems.Find(i => i.Item.Equals(enemyController));
        if (pooledItem != null)
        {
            pooledItem.IsUsed = false;
        }
    }
}