using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour
{
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Rigidbody tankRb;
    [SerializeField] private BulletService bulletService;
    [SerializeField] private Button shootButton;
    [SerializeField] private LevelDestroyer levelDestroyer;

    private TankController tankController;
    private float movement;
    private float rotation;


    private void Start()
    {        
        shootButton.onClick.AddListener(Shoot);
    }
    private void Update()
    {
        Movement();

        if (movement != 0) 
        {
            tankController.Move(movement, 5);
        }
        if (rotation != 0)
        {
            tankController.Rotate(rotation, 30);
        }
    }

    private void Movement()
    {
        movement = joyStick.Vertical;
        rotation = joyStick.Horizontal;
    }
    public Rigidbody GetRigidbody() { return tankRb; }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
    public BulletService GetBulletService()
    {
        return bulletService;
    }
    public void Shoot()
    {
        AudioClip clip = tankController.GetTankModel().shootClip;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        tankController.ShootBullet();
    }

    
}
