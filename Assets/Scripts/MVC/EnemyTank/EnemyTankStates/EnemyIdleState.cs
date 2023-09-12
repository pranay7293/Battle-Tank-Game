using UnityEngine;

public class EnemyIdleState : EnemyStates
{
    private float TimeElapsed { get; set; } = 0f;
    private float idleTime = 2f;
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
        if (TimeElapsed > idleTime)
        {
            EnemyView.ChangeState(EnemyView.patrolState);
        }
        TimeElapsed += Time.deltaTime;
    }
}
