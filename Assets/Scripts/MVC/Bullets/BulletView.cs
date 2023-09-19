using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController BulletController { get; set; }
    public BulletPool playerBulletPool;
    public BulletPool enemyBulletPool;
    private Rigidbody rb;

    private bool hasExploded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerBulletPool = TankService.Instance.TankController.TankView.GetBulletService().GetComponent<BulletPool>();
        enemyBulletPool = EnemyService.Instance.EnemyController.EnemyView.GetBulletService().GetComponent<BulletPool>();
       
    }

    public void SetBulletController(BulletController bulletCtrl)
    {
        BulletController = bulletCtrl;
    }
    public BulletController GetBulletController()
    {
        return BulletController;
    }
    public Rigidbody GetRigidbody()
    {
        return rb;
    }


    private void OnCollisionEnter(Collision collision)
    {
        BulletType bulletType = BulletController.GetBulletType();
        TankView tankView = collision.gameObject.GetComponent<TankView>();
        EnemyView enemyView = collision.gameObject.GetComponent<EnemyView>();

        if (bulletType == BulletType.PlayerBullet)
        {
            if (tankView != null)
            {
                DisablePlayerBullet(); //Do Nothing, When Player Bullet hits another Player
            }
            else
            {
                BulletExplode();
                DisablePlayerBullet();
            }
        }
        else if (bulletType == BulletType.EnemyBullet)
        {
            if (enemyView != null)
            {
                DisableEnemyBullet(); //Do Nothing, When Enemy Bullet hits another Enemy
            }
            else
            {
                BulletExplode();
                DisableEnemyBullet();
            }
        }
    }

    private void DisableEnemyBullet()
    {
        SoundManager.Instance.PlaySound(Sounds.ShotExplosion);
        Disable();
        enemyBulletPool.ReturnItem(BulletController);
    }

    private void DisablePlayerBullet()
    {
        SoundManager.Instance.PlaySound(Sounds.ShotExplosion);
        Disable();
        playerBulletPool.ReturnItem(BulletController);
    }

    public void BulletExplode()
    {
        if (!hasExploded)
        {
            BulletModel model = BulletController.GetBulletModel();

            ParticleSystem explosion = Instantiate(model.bulletExplosion, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, 2f);
            hasExploded = true;
        }
        hasExploded = false;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

}   