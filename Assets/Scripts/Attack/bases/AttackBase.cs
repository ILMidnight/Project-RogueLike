using UnityEngine;

public class AttackBase : IAttack
{
    protected AttackController aController;

    protected LayerMask skipMask;
    protected Transform pool;

    public AttackBase(AttackController aController)
    {
        this.aController = aController;
        skipMask = aController.pMng.skipMask;
        pool = aController.pMng.attackPool.transform;
    }

    public virtual void Attack()
    {
        
    }

    public virtual void Tick()
    {
        
    }
}
