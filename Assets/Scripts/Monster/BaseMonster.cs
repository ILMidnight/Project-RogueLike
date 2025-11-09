using Unity.VisualScripting;
using UnityEngine;

public class BaseMonster : MonoBehaviour, IMonster, IEntity
{
    protected MonsterManager mMng;

    protected int currentHp = 100;

    protected int deathExp = 100;

    public Rigidbody rb;

    public virtual void Inintialize(MonsterManager mMng)
    {
        this.mMng = mMng;
        SetUp();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void SetUp()
    {
        currentHp = 100 + (int)GameSceneManager.GetCurrentDifficulty();
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Debug.Log($"Create {name}");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Hit(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);
        if(currentHp <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log($"Death {name}");
        mMng.pMng.AddExp(deathExp);
        Destroy(gameObject);
    }
}
