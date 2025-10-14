using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class AttackController : PlayerControllerBase
{
    Dictionary<string, IAttack> attacks;

    InputController inputController;

    public Transform attackParent;

    public AttackController(PlayerManager pMng) : base(pMng)
    {
        inputController = pMng.states["InputController"] as InputController;

        attackParent = pMng.transform.GetChild(0);

        attacks = new Dictionary<string, IAttack>();
        attacks.Add("HitScan", new HitScanAttack(this));
    }

    public override void Tick()
    {
        base.Tick();

        // foreach(var attack in attacks)
        // {
        //     attack.Tick
        // }

        if (inputController.inputClick)
        {
            inputController.CheckClick();
            foreach(var attack in attacks)
            {
                attack.Value.Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
            attacks.Remove("HitScan");
    }
}
