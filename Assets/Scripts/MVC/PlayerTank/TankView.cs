using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankView : Subject, IDamagable
{
    [SerializeField] private Joystick joyStick;
    [SerializeField] private Button shootButton;
    [SerializeField] private Button replyButton;
    [SerializeField] private Button exitButton1;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton2;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Rigidbody tankRb;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private BulletService bulletService;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private TextMeshProUGUI enemiesCount;
    [SerializeField] private GameObject levelComplete;      
    [SerializeField] private GameObject pauseMenu;      
    private TankController TankController { get; set; }
    private float movement;
    private float rotation;
    private TankModel TankModel;
    private int dealDamage;

   
    private void Start()
    {
        enemiesCount.text = "Enemies Left:" + EnemyService.Instance.ListofEnemies.Count;
        shootButton.onClick.AddListener(Shoot);
        resumeButton.onClick.AddListener(ResumePlay);
        pauseButton.onClick.AddListener(PausePlay);
        replyButton.onClick.AddListener(LoadLevel);
        exitButton1.onClick.AddListener(LoadLobby);
        exitButton2.onClick.AddListener(LoadLobby);
        TankModel = TankController.GetTankModel();
        dealDamage = TankModel.DealDamage;
        healthBar.UpdateHealthBar(TankModel.TankHealth);
    }

    private void Update()
    {
        Movement();
        if (movement != 0)
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

    public Transform GetBulletSpawnTransform()
    { return bulletSpawn; }

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
    public void LoadLobby()
    {
        SoundManager.Instance.PlaySound(Sounds.ExitButtonClick);
        SceneManager.LoadScene(0);
        levelComplete.SetActive(false);
        pauseMenu.SetActive(false);
    }
    private void PausePlay()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumePlay()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadLevel()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayButtonClick);
        SceneManager.LoadScene(1);
    }

    public void Shoot()
    {
        NotifyBulletsFiredObservers();
        TankController.ShootBullet();
        SoundManager.Instance.PlaySound(Sounds.ShotFire);
    }
    private void OnCollisionEnter(Collision collision)
    {
        BulletView bulletView = collision.gameObject.GetComponent<BulletView>();

        if (bulletView != null)
        {
            BulletController bulletController = bulletView.GetBulletController();

            if (bulletController != null)
            {
                BulletType bulletType = bulletController.GetBulletType();

                if (bulletType == BulletType.EnemyBullet)
                {
                    TakeDamage(dealDamage);
                }
            }
        }
    }
    public void DestroyTank()
    {
        EventManager.TankDestroyed();
        if (TankController != null)
        {
           ParticleSystem explosion = Instantiate(TankModel.Explosion, gameObject.transform.position, Quaternion.identity);
            explosion.Play();
            SoundManager.Instance.PlaySound(Sounds.TankExplosion);
            Destroy(gameObject);
            Destroy(explosion.gameObject, 1.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        TankController.TakeDamage(damage);
    }

    public void UpdateEnemiesCount()
    {
        if(EnemyService.Instance.ListofEnemies.Count > 0)
        {
            enemiesCount.text = "Enemies Left:" + EnemyService.Instance.ListofEnemies.Count;
        }
        else
        {
            enemiesCount.text = "Enemies Left:" + EnemyService.Instance.ListofEnemies.Count;
            Invoke(nameof(LevelCompleteMenu), 1f);
        }
    }
    private void LevelCompleteMenu()
    {
        SoundManager.Instance.PlaySound(Sounds.PlayerWin);
        levelComplete.SetActive(true);
    }

}
