using UnityEngine;
using Random = UnityEngine.Random;

public class BulletService : MonoBehaviour
{
    [SerializeField] private BulletScriptableObjectList bulletList;
    public BulletController BulletController { get; private set; }


    public BulletController SpawnBullet(Transform spawn, BulletType bulletType)
    {
        int randomNumber = (int)Random.Range(0f, bulletList.bullets.Length - 1);
        BulletScriptableObject obj = bulletList.bullets[randomNumber];
        BulletModel bulletModel = new BulletModel(obj);
        BulletController = new BulletController(obj.bulletView, bulletModel, spawn, bulletType);
        return BulletController;
    }
}
