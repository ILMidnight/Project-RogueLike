using System.IO;
using UnityEngine;

public class HitScanAttack : AttackBase
{
    Vector3 targetPoint;

    RaycastHit hit;

    Transform bulletObject;

    int totalBullet;
    int currentBullet;

    float currentTime;
    

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

        // if(currentTime)


        // currentTime = Time.deltaTime;
    }

    public override void Tick()
    {
        base.Tick();

        currentTime += Time.deltaTime;

        if (!aController.pInput.stillMouseDown)
            return;

        if (currentTime < 1 / aController.pState.baseStatus.AttackSpeed)
            return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, ~skipMask))
        {
            bulletObject.position = hit.point;
            var temp = hit.transform.GetComponent<BaseMonster>();

            if (temp != null)
            {
                // Debug.Log(temp);
                temp.Hit((int)aController.pState.baseStatus.Damage);
            }
        }
        else
        {
            bulletObject.position = aController.attackParent.position - (Vector3.down * 20);
        }

        currentTime = 0;
    }
}
