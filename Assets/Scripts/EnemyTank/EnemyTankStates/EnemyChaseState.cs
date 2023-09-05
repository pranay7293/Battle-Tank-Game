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
        if (EnemyView && TankCtrl != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TankCtrl.TankView.transform.position, EnemyView.EnemyModel.EnemySpeed * Time.deltaTime * 0.25f);
            transform.LookAt(TankCtrl.TankView.transform);
        }
    }
}
