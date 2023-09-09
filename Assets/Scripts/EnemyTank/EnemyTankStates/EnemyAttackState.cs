using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    private float TimeElapsed { get; set; } = 0f;

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
        if (TankCtrl != null && EnemyView != null)
        {
            transform.LookAt(TankCtrl.TankView.transform.position);

            if (TimeElapsed > 1.5f)
            {
                EnemyView.EnemyController.Fire();
                TimeElapsed = 0f;
            }
            if (TimeElapsed <= 1.5f)
            {
                TimeElapsed += Time.deltaTime;
            }
        }
    }

}
