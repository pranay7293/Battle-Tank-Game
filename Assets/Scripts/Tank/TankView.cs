using UnityEngine;
using UnityEngine.UI;

public class TankView : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody tankRb;
    [SerializeField] private BulletService bulletSpawner;
    [SerializeField] private Button shootButton;

    private TankController tankController;
    private float movement;
    private float rotation;


    private void Start()
    {          
        shootButton.onClick.AddListener(Shoot);
        Debug.Log("Tank view created");
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
        movement = joystick.Vertical;
        rotation = joystick.Horizontal;
    }
    public Rigidbody GetRigidbody() { return tankRb; }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
    public BulletService GetBulletSpawner()
    {
        return bulletSpawner;
    }
    public void Shoot()
    {
        AudioClip clip = tankController.GetTankModel().shootClip;
        gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
        tankController.ShootBullet();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //
        }
    }
}
