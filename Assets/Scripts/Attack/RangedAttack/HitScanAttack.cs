using System.IO;
using UnityEngine;

public class HitScanAttack : AttackBase
{
    Vector3 targetPoint;

    RaycastHit hit;

    Transform bulletObject;

    public HitScanAttack(AttackController aController) : base(aController)
    {
        bulletObject = GameObject.Instantiate(
            Resources.Load<GameObject>(Path.Combine("Prefabs", "Bullet")),
            aController.pMng.transform.position - (Vector3.down * 20),
            Quaternion.identity,
            pool
        ).transform;
    }

    public override void Attack()
    {
        base.Attack();

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ~skipMask))
        {
            bulletObject.position = hit.point;
        }
        else
        {
            bulletObject.position = aController.attackParent.position - (Vector3.down * 20);
        }
        
    }
}
