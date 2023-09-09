using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public abstract class EnemyStates : MonoBehaviour
{
    protected EnemyView EnemyView { set; get; }
    protected TankController TankCtrl { set; get; } = null;
    private void Start()
    {
        EnemyView = GetComponent<EnemyView>();
        TankCtrl = TankService.Instance.TankController;
    }

    public virtual void OnEnterState()
    {
        this.enabled = true;
    }
    public virtual void OnExitState()
    {
        this.enabled = false;
    }
    public abstract void Tick();
}
