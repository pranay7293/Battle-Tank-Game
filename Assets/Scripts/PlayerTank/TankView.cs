using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour
{
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Rigidbody tankRb;
    [SerializeField] private BulletService bulletService;
    [SerializeField] private Button shootButton;
    [SerializeField] private LevelDestroyer levelDestroyer;

    private TankController TankController { get; set; }
    private float movement;
    private float rotation;
    private TankModel TankModel;


    private void Start()
    {        
        shootButton.onClick.AddListener(Shoot);
        TankModel = TankController.GetTankModel();
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
    public void Shoot()
    {
        AudioClip clip = TankController.GetTankModel().shootClip;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        TankController.ShootBullet();
    }
    public void DestroyTank()
    {
        LevelDestroyer.Instance.IsDead = true;
        if (TankController != null)
        {
           ParticleSystem explosion = Instantiate(TankModel.Explosion, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
            Destroy(explosion, 1.5f);
        }
    }


}
