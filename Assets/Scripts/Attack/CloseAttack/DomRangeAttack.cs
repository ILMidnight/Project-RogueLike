using System.IO;
using UnityEngine;

public class DomRangeAttack : AttackBase
{
    Transform rangeDom;

    float currentTime;

    // float 

    public DomRangeAttack(AttackController aController) : base(aController)
    {
        rangeDom = GameObject.Instantiate(
            Resources.Load<GameObject>(Path.Combine("Prefabs", "Attack", "RangeDom")),
            aController.pMng.transform.position,
            Quaternion.identity,
            aController.pMng.transform
        ).transform;

        rangeDom.gameObject.SetActive(false);

        rangeDom.localScale = Vector3.one * aController.pState.baseStatus.attackRange;

        Debug.Log(aController.pState.baseStatus.attackRange);

        currentTime = 0;
    }

    public override void Tick()
    {
        base.Tick();

        currentTime += Time.deltaTime;

        if (10 / aController.pState.baseStatus.AttackSpeed < currentTime)
        {
            currentTime = 0;
            AutoAttack();
        }
    }

    void AutoAttack()
    {
        rangeDom.gameObject.SetActive(true);
        var tempList = Physics.OverlapSphere(aController.pMng.transform.position, aController.pState.baseStatus.attackRange);
        foreach(var t in tempList)
        {
            var monster = t.GetComponent<BaseMonster>();
            if (monster == null) continue;
            monster.Hit((int)aController.pState.baseStatus.Damage / 2);
        }
    }
}
