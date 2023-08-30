using UnityEngine;
using Random = UnityEngine.Random;

public class BulletService : MonoBehaviour 
{
    [SerializeField] private BulletScriptableObjectList bulletList;

    public void SpawnBullet(Transform spawn)
    {
        int randomNumber = (int)Random.Range(0f, bulletList.bullets.Length - 1);
        BulletScriptableObject obj = bulletList.bullets[randomNumber];
        BulletModel bulletModel = new BulletModel(obj);
        BulletController bulletController = new BulletController(obj.bulletView, bulletModel, spawn);
    }
}
