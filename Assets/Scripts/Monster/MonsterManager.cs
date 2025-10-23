using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

enum MonsterTypeID
{
    Walker = 1,

}

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    Transform monsterSpawnTrans;

    [SerializeField]
    public PlayerManager pMng;

    [SerializeField]
    GameObject testPrefab;

    [SerializeField]
    GameObject temp;

    List<BaseMonster> monsters;

    private void Start()
    {
        monsters = new List<BaseMonster>();

        // AddMonster((MonsterTypeID)1);

        InvokeRepeating("SpawnMonsterForInvoke", 0, 3.5f);
    }

    private BaseMonster AddMonster(MonsterTypeID typeID)
    {
        
        temp = Instantiate(
            Resources.Load<GameObject>(Path.Combine("Prefabs", "Monsters", typeID.ToString())),
            new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f)),
            Quaternion.identity, monsterSpawnTrans);
        monsters.Add(temp.GetComponent<BaseMonster>());
        return temp.GetComponent<BaseMonster>();
    }

    private void SpawnMonsterForInvoke()
    {
        // Debug.Log($"currentLevel : {GameSceneManager.GetCurrentDifficulty()}");
        for(int i = 0; i < (int)GameSceneManager.GetCurrentDifficulty(); i++)
        {
            var monster = AddMonster((MonsterTypeID)1);
            monster.Inintialize(this);
        }
    }

    private void Update()
    {
        
    }
}
