using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : PlayerControllerBase
{
    Dictionary<string, IAttack> attacks;

    public InputController pInput;

    public Transform attackParent;

    public PlayerStatusController pState;

    public UnityEvent<Transform> monsterDeathEvent;

    public AttackController(PlayerManager pMng) : base(pMng)
    {
        
    }

    public override void InitController()
    {
        base.InitController();

        pInput = pMng.states["InputController"] as InputController;
        pState = pMng.states["StatusController"] as PlayerStatusController;

        attackParent = pMng.transform.GetChild(0);

        attacks = new Dictionary<string, IAttack>();
        // attacks.Add("HitScan", new HitScanAttack(this));
        // attacks.Add("RangeDom", new DomRangeAttack(this));
        attacks.Add("BombLauncher", new BombLauncher(this));

        monsterDeathEvent = new UnityEvent<Transform>();
        monsterDeathEvent.AddListener((deathData) =>
        {
            DeathBomb(deathData);
        });
    }

    public override void Tick()
    {
        base.Tick();

        if (pInput.inputClick)
        {
            pInput.CheckClick();
            foreach (var attack in attacks)
            {
                attack.Value.Attack();
            }
        }

        foreach (var attack in attacks)
        {
            attack.Value.Tick();
        }

        if (Input.GetKeyDown(KeyCode.P))
            attacks.Remove("HitScan");
    }

    public void DeathMonster(Transform deathMonster)
    {
        monsterDeathEvent.Invoke(deathMonster);
    }

    void DeathBomb(Transform deathMonster)
    {
        var temp = pMng.attackPool.moveBombPool.Get();
        temp.SetBomb(
            (int)pState.baseStatus.Damage / 3,
            0,
            deathMonster.position,
            Vector2.zero
        );
    }
}
