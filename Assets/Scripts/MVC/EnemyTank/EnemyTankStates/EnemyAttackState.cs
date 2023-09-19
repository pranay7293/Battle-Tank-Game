using UnityEngine;

public class EnemyAttackState : EnemyStates
{
    private float TimeElapsed { get; set; } = 0f;
    private float attackReset = 1.5f;
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
            transform.LookAt(TankCtrl.TankView.transform);

            if (TimeElapsed > attackReset)
            {
                EnemyView.EnemyController.Fire();
                TimeElapsed = 0f;
            }
            if (TimeElapsed <= attackReset)
            {
                TimeElapsed += Time.deltaTime;
            }
        }
    }

}
