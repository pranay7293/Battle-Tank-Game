using UnityEngine;

public class TankController
{
    private TankModel TankModel { get; }
    private TankView TankView { get; }
    private BulletService bulletSpawner;
    private Rigidbody rb;

    public TankController(TankModel _tankModel, TankView tankPrefab)
    {
        TankModel = _tankModel;
        TankView = GameObject.Instantiate<TankView>(tankPrefab);
        TankModel.SetTankController(this);
        TankView.SetTankController(this);
        rb = TankView.GetRigidbody();
        bulletSpawner = TankView.GetBulletSpawner();

    }

    public void Move(float movement, float movementSpeed)
    {
        Vector3 move = TankView.transform.forward * movement * movementSpeed * Time.deltaTime;
        rb.MovePosition(TankView.transform.position + move);
    }
    public void Rotate(float rotate, float rotateSpeed)
    {
        Vector3 vector = new Vector3(0, rotate*rotateSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public TankModel GetTankModel()
    {
        return TankModel;
    }
    public void ShootBullet()
    {
        bulletSpawner.SpawnBullet(bulletSpawner.transform);
    }
}
