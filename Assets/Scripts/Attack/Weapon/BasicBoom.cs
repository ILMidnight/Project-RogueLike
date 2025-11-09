using System.Collections;
using UnityEngine;

public class BasicBoom : MonoBehaviour
{
    SphereCollider[] colliders;

    [SerializeField]
    float boomDelayTime;

    private void Awake()
    {
        colliders = new SphereCollider[2];

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = gameObject.AddComponent<SphereCollider>();
        }

        colliders[0].isTrigger = true;
        colliders[0].radius = .65f;
    }

    IEnumerator DelayBoom()
    {
        yield return new WaitForSeconds(boomDelayTime);

        
    }
    
    private void OnTriggerEnter(Collider other) {
        var temp = other.GetComponent<BaseMonster>();
    }
}
