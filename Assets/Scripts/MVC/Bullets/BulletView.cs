using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController BulletController { get; set; }
    public BulletPool playerBulletPool;
    public BulletPool enemyBulletPool;

    private bool hasExploded = false; 
    private AudioSource audioSource; 
    
    private void Start()
    {
        playerBulletPool = TankService.Instance.TankController.TankView.GetBulletService().GetComponent<BulletPool>();
        enemyBulletPool = EnemyService.Instance.EnemyController.EnemyView.GetBulletService().GetComponent<BulletPool>();
        audioSource = GetComponent<AudioSource>();

        BulletController.ShootBullet();
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
        return GetComponent<Rigidbody>();
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
                Invoke(nameof(DisablePlayerBullet), 1); //Do Nothing, When Player Bullet hits another Player
            }
            else
            {
                BulletExplode();
                Invoke(nameof(DisablePlayerBullet), 1);
            }   
        }
        else if (bulletType == BulletType.EnemyBullet)
        {
            if (enemyView != null)
            {
                Invoke(nameof(DisableEnemyBullet), 1); //Do Nothing, When Enemy Bullet hits another Enemy
            }
            else
            {
                BulletExplode();
                Invoke(nameof(DisableEnemyBullet), 1);
            }
        }       
    }

    private void DisableEnemyBullet()
    {
        Disable();
        enemyBulletPool.ReturnItem(BulletController);

    }

    private void DisablePlayerBullet()
    {
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

            audioSource.PlayOneShot(BulletController.GetBulletModel().bulletClip);

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

    public void SetTransform(Transform transform)
    {
        gameObject.transform.position = transform.position;
        gameObject.transform.forward = transform.forward;
        gameObject.transform.LookAt(transform.forward * 100);
    }
}
