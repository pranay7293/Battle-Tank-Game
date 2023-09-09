using UnityEngine;

public class BulletView : MonoBehaviour
{
    private BulletController BulletController { get; set; }
    private bool hasExploded = false; 
    private AudioSource audioSource;
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
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BulletController.ShootBullet();
        Invoke(nameof(DestroyBullet), 3f);
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        BulletType bulletType = BulletController.GetBulletType();

        if (collision.gameObject.CompareTag("Player") && bulletType == BulletType.PlayerBullet)
        {
            DestroyBullet();
        }
        else if (collision.gameObject.CompareTag("Enemy") && bulletType == BulletType.EnemyBullet)
        {
            DestroyBullet();
        }
        else
        {
            if (!hasExploded)
            {
                BulletModel model = BulletController.GetBulletModel();

                ParticleSystem explosion = Instantiate(model.bulletExplosion, transform.position, Quaternion.identity);
                explosion.Play();

                audioSource.PlayOneShot(BulletController.GetBulletModel().bulletClip);
                
                Destroy(explosion.gameObject, 2f);
                DestroyBullet();
                hasExploded = true;
            }
            hasExploded = false;
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
