using UnityEngine;

public class BaseMonster : MonoBehaviour, IMonster, IEntity
{
    protected int currentHp = 100;

    public virtual void Inintialize()
    {
        SetUp();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void SetUp()
    {
        currentHp = 100 + (int)GameSceneManager.GetCurrentDifficulty();
        Debug.Log($"Create {name}");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Hit(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);
        Debug.Log(currentHp);
        if(currentHp <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log($"Death {name}");
        Destroy(gameObject);
    }
}
