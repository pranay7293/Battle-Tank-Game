using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController BulletController { get; set; }

    public void SetBulletController(BulletController bulletController)
    {
        BulletController = bulletController;
    }
    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }
    private void Start()
    {
        BulletController.ShootBullet();
        Invoke(nameof(DestroyBullet), 4f);
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        //explode
        if(collision.gameObject.GetComponent<TankView>() == null)
        {
            BulletModel model = BulletController.GetBulletModel();

            ParticleSystem explosion = Instantiate(model.explosionType, transform.position, Quaternion.identity);
            explosion.Play(); 

            if (gameObject != null)
                BulletController.GetBulletModel().explosionSource.PlayOneShot(BulletController.GetBulletModel().explosionClip);

            Destroy(explosion, 2f);
            Destroy(gameObject);            
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
