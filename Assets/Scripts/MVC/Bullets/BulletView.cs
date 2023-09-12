using System;
using System.Collections;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController BulletController { get; set; }
    private BulletPool playerBulletPool;
    private BulletPool enemyBulletPool;

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

        if (this.gameObject.CompareTag("PlayerBullet"))
        {
            if (collision.gameObject.CompareTag("Player")) 
            {
                StartCoroutine(DisablePlayerBullet(2)); //Do Nothing, When Player Bullet hits another Player
            }
            else
            {
                BulletExplode();
                StartCoroutine(DisablePlayerBullet(2));
            }   
        }
        else if (this.gameObject.CompareTag("EnemyBullet"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(DisableEnemyBullet(2)); //Do Nothing, When Enemy Bullet hits another Enemy
            }
            else
            {
                BulletExplode();
                StartCoroutine(DisableEnemyBullet(2));
            }
        }       
    }

    private IEnumerator DisableEnemyBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        Disable();
        enemyBulletPool.ReturnItem(BulletController);

    }

    private IEnumerator DisablePlayerBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
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
