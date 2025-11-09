using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private List<T> poolList;
    private Transform parent;

    public ObjectPool(T prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        poolList = new List<T>();
    }

    public void PreLoad(int count = 10)
    {
        for (int i = 0; i < count; i++)
        {
            poolList.Add(Object.Instantiate(prefab, parent));
            poolList[i].gameObject.SetActive(false);
        }
    }

    public T Get()
    {
        foreach (var attack in poolList)
        {
            if (!attack.gameObject.activeSelf)
            {
                return attack;
            }
        }

        var tempAttack = Object.Instantiate(prefab, parent);
        poolList.Add(tempAttack);
        return tempAttack;
    }

    public void Return(T target)
    {
        target.gameObject.SetActive(false);
    }
}

public class AttackObejctPool : MonoBehaviour
{
    public ObjectPool<MovementBomb> moveBombPool;

    private void Awake()
    {
        moveBombPool = new ObjectPool<MovementBomb>(
            Resources.Load<GameObject>(Path.Combine("Prefabs", "Attack", "MovementBomb")).GetComponent<MovementBomb>(),
            transform
        );

        moveBombPool.PreLoad();
    }
}
