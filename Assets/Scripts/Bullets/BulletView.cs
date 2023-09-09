using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController BulletController { get; set; }

    public void SetBulletController(BulletController bulletCtrl)
    {
        BulletController = bulletCtrl;
    }
    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }
    private void Start()
    {
        BulletController.ShootBullet();
        Invoke(nameof(DestroyBullet), 3f);
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        //explode
        if(collision.gameObject.GetComponent<TankView>() == null)
        {
            BulletModel model = BulletController.GetBulletModel();

            ParticleSystem explosion = Instantiate(model.bulletExplosion, transform.position, Quaternion.identity);
            explosion.Play(); 

            if (gameObject != null)
                BulletController.GetBulletModel().bulletAudioSource.PlayOneShot(BulletController.GetBulletModel().bulletClip);

            Destroy(explosion.gameObject, 2f);
            Destroy(gameObject);            
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
