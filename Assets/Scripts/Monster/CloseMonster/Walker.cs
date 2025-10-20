using Unity.VisualScripting;
using UnityEngine;

public class Walker : BaseMonster
{
    Vector3 targetPos;

    [SerializeField]
    float speed = .5f;

    public override void Inintialize()
    {
        base.Inintialize();
    }

    protected override void SetUp()
    {
        base.SetUp();
        // Debug.Log(currentHp);
        // currentHp = 
    }

    private void OnTriggerStay(Collider other) {
        
        if(other.GetComponent<PlayerManager>())
        {
            other.GetComponent<PlayerManager>().Hit(5);
        }
    }  

    public override void Update()
    {
        targetPos = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * speed
        );

        transform.GetChild(0).LookAt(Camera.main.transform.position);
    }
}
