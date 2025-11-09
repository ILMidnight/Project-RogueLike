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

    public void ShootBomb(Transform trans, float power)
    {
        transform.position = trans.position + trans.forward;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(trans.forward * power, ForceMode.Impulse);
    }
}
