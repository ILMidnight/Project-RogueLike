using Unity.VisualScripting;
using UnityEngine;

public class MovementBomb : BasicBomb
{
    Rigidbody rb;
    protected override void Awake()
    {
        base.Awake();

        rb = transform.AddComponent<Rigidbody>();
    }

    public void SetBomb(int Damage, float Range, Vector3 basePos, Vector3 targetDir)
    {
        base.SetBomb(Damage, Range, basePos);

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(targetDir * Range, ForceMode.Impulse);
    }

    // public void ShootBomb(Transform trans, float power)
    // {
    //     transform.position = trans.position + trans.forward;
    //     rb.linearVelocity = Vector3.zero;
    //     rb.AddForce(trans.forward * power, ForceMode.Impulse);
    // }
}
