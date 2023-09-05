using UnityEngine;

public class TankController
{
    private TankModel TankModel { get; }
    public TankView TankView { get; }
    private readonly BulletService bulletService;
    private readonly Rigidbody tankRb;

    public TankController(TankModel _tankModel, TankView _tankView)
    {
        TankModel = _tankModel;
        TankView = GameObject.Instantiate<TankView>(_tankView);
        TankView.SetTankController(this);
        tankRb = TankView.GetRigidbody();
        bulletService = TankView.GetBulletService();

    }

    public void Move(float movement, float movementSpeed)
    {
        Vector3 move = movement * movementSpeed * Time.deltaTime * TankView.transform.forward;
        tankRb.MovePosition(TankView.transform.position + move);
    }
    public void Rotate(float rotate, float rotateSpeed)
    {
        Vector3 vector = new Vector3(0, rotate*rotateSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        tankRb.MoveRotation(tankRb.rotation * deltaRotation);
    }

    public TankModel GetTankModel()
    {
        return TankModel;
    }
    public void ShootBullet()
    {
        bulletService.SpawnBullet(bulletService.transform);
    }
}
