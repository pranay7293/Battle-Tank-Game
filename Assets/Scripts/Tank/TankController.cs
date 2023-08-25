using UnityEngine;

public class TankController
{
    private TankView tankView;
    private TankModel tankModel;
    private Rigidbody rb;

    public TankController(TankModel _tankModel, TankView tankPrefab)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(tankPrefab);        
        tankModel.SetTankController(this);
        tankView.SetTankController(this);
        rb = tankView.GetRigidbody();
    }

    public void Move(float movement, float movementSpeed)
    {
        Vector3 move = tankView.transform.forward * movement * movementSpeed * Time.deltaTime;
        rb.MovePosition(tankView.transform.position + move);
    }
    public void Rotate(float rotate, float rotateSpeed)
    {
        Vector3 vector = new Vector3(0, rotate*rotateSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

    }
}
