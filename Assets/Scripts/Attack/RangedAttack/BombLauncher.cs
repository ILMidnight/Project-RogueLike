using UnityEngine;

public class BombLauncher : HitScanAttack
{
    public BombLauncher(AttackController aController) : base(aController)
    {

    }

    protected override void InItSetting(AttackController aController)
    {
        // base.InItSetting(aController);
    }

    protected override float CoolTime()
    {
        return base.CoolTime() * 1.75f;
    }

    public override void Tick()
    {
        base.Tick();
    }

    protected override void Shoot()
    {
        // base.Shoot();
        currentTime = 0;

        var temp = aController.pMng.attackPool.moveBombPool.Get();
        temp.gameObject.SetActive(true);
        temp.ShootBomb(Camera.main.transform, aController.pState.baseStatus.attackRange / 7);
    }
}
