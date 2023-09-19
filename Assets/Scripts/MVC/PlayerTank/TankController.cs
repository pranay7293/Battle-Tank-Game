using UnityEngine;

public class TankController
{
    private TankModel TankModel { get; }
    public TankView TankView { get; }
    private BulletService bulletService;
    private Transform bulletSpawn;
    private readonly Rigidbody tankRb;
    
    public TankController(TankModel _tankModel, TankView _tankView)
    {
        TankModel = _tankModel;
        TankView = GameObject.Instantiate<TankView>(_tankView);
        TankView.SetTankController(this);
        tankRb = TankView.GetRigidbody();
        bulletService = TankView.GetBulletService();
        bulletSpawn = TankView.GetBulletSpawnTransform();

    }

    public void TankMove(float movement)
    {
        Vector3 move = movement * TankModel.TankSpeed * Time.deltaTime * TankView.transform.forward;
        tankRb.MovePosition(TankView.transform.position + move);
    }

    public void TankRotate(float rotate)
    {
        Quaternion targetRotation = Quaternion.Euler(0f, rotate * 45 * Time.deltaTime, 0f);
        tankRb.MoveRotation(targetRotation * tankRb.rotation);
    }

    public TankModel GetTankModel()
    {
        return TankModel;
    }
    public void ShootBullet()
    {
        bulletService.SpawnBullet(bulletSpawn.transform, BulletType.PlayerBullet);
    }
    public void TakeDamage(int damage)
    {
        if (TankModel.TankHealth - damage <= 0)
        {
           TankView.GetHealthBar().UpdateHealthBar(0);
            TankView.DestroyTank();
        }
        else
        {
            TankModel.TankHealth -= damage;
            TankView.GetHealthBar().UpdateHealthBar(TankModel.TankHealth);
        }
    }

}
