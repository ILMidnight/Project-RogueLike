using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BasicBomb : MonoBehaviour
{
    SphereCollider[] colliders;

    protected UnityEvent explodeEvent;

    [SerializeField]
    protected float bombDelayTime = 7;

    protected int bombDamage = 10;
    protected float bombRange = 1;

    protected virtual void Awake()
    {
        colliders = new SphereCollider[2];

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = gameObject.AddComponent<SphereCollider>();
        }

        colliders[0].isTrigger = true;
        colliders[0].radius = .65f;

        explodeEvent = new UnityEvent();
        explodeEvent.AddListener(() =>
        {
            ExplodeBomb();
        });
    }

    public virtual void SetBomb(int Damage, float Range, Vector3 basePos)
    {
        bombDamage = Damage;
        bombRange = Range;
        transform.position = basePos;

        gameObject.SetActive(true);
    }

    private void OnEnable() {
        colliders[0].enabled = true;
        StartCoroutine(DelayBomb());
    }

    IEnumerator DelayBomb()
    {
        yield return new WaitForSeconds(bombDelayTime);
        explodeEvent.Invoke();
    }

    private void ExplodeBomb()
    {
        StopAllCoroutines();
        colliders[0].enabled = false;

        var tempList = Physics.OverlapSphere(transform.position, bombRange);
        foreach (var tempHit in tempList)
        {
            var tempMonster = tempHit.GetComponent<BaseMonster>();
            if (tempMonster != null)
            {
                tempMonster.Hit(bombDamage);
                tempMonster.rb.AddExplosionForce(bombDamage, transform.position, bombRange);
            }
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BaseMonster>())
        {
            explodeEvent.Invoke();
        }
    }
}
