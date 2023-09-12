using UnityEngine;
using UnityEngine.UI;

public class TankView : Subject, IDamagable
{
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Rigidbody tankRb;
    [SerializeField] private BulletService bulletService;
    [SerializeField] private Button shootButton;
    [SerializeField] private LevelDestroyer levelDestroyer;
    [SerializeField] private HealthBar healthBar;

    private TankController TankController { get; set; }
    private float movement;
    private float rotation;
    private TankModel TankModel;
    private int dealDamage;


    private void Start()
    {
        EventManager eventManager = FindObjectOfType<EventManager>();
        shootButton.onClick.AddListener(Shoot);
        TankModel = TankController.GetTankModel();
        dealDamage = TankModel.DealDamage;
        healthBar.UpdateHealthBar(TankModel.TankHealth);


    }
    private void Update()
    {
        Movement();

        if (movement != 0 ) 
        {
            TankController.TankMove(movement);
        }
        if (rotation != 0) 
        {
            TankController.TankRotate(rotation);
        }
    }
    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position + new Vector3(-40f, 40f, -25f);
    }

    private void Movement()
    {
        movement = joyStick.Vertical;
        rotation = joyStick.Horizontal;
    }
    public Rigidbody GetRigidbody() { return tankRb; }

    public void SetTankController(TankController _tankController)
    {
        TankController = _tankController;
    }
    public BulletService GetBulletService()
    {
        return bulletService;
    }
    public HealthBar GetHealthBar()
    {
        return healthBar;
    }
    public void Shoot()
    {
        NotifyBulletsFiredObservers();
        AudioClip clip = TankController.GetTankModel().ShootClip;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        TankController.ShootBullet();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(dealDamage);
        }
    }
    public void DestroyTank()
    {
        EventManager.TankDestroyed();

        LevelDestroyer.Instance.IsDead = true;
        if (TankController != null)
        {
           ParticleSystem explosion = Instantiate(TankModel.Explosion, gameObject.transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(gameObject);
            Destroy(explosion.gameObject, 1.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        TankController.TakeDamage(damage);
    }
}
