using UnityEngine;

public class TankController
{
    private TankModel TankModel { get; }
    public TankView TankView { get; }
    private readonly BulletService BulletService;
    private readonly Rigidbody tankRb;
    public TankController(TankModel _tankModel, TankView _tankView)
    {
        TankModel = _tankModel;
        TankView = GameObject.Instantiate<TankView>(_tankView);
        TankView.SetTankController(this);
        tankRb = TankView.GetRigidbody();
        BulletService = TankView.GetBulletService();

    }

    public void TankMove(float movement)
    {
        Vector3 move = movement * TankModel.TankSpeed * Time.deltaTime * TankView.transform.forward;
        tankRb.MovePosition(TankView.transform.position + move);
    }

    public void TankRotate(float rotate)
    { 
        Vector3 vector = new Vector3(0, rotate*100, 0);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        tankRb.MoveRotation(tankRb.rotation * deltaRotation);
    }

    public TankModel GetTankModel()
    {
        return TankModel;
    }
    public void ShootBullet()
    {
        BulletService.SpawnBullet(BulletService.transform, BulletType.PlayerBullet);
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
