using UnityEngine;

public class EnemyChaseState : EnemyStates
{
    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }


    public override void Tick()
    {
        if (EnemyView != null && TankCtrl != null)
        {
            Vector3 direction = (TankCtrl.TankView.transform.position - transform.position).normalized;

            transform.position += EnemyView.EnemyModel.EnemySpeed * Time.deltaTime * direction;
            transform.LookAt(TankCtrl.TankView.transform.position);
        }
    }
}
