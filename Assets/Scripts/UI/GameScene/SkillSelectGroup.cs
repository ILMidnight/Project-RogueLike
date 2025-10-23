using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillSelectGroup : MonoBehaviour
{
    [SerializeField]
    GameObject skillSelectButtonPrefab;

    List<SkillSelectButton> buttonList;

    private void Awake() {
        UnityEvent temp = new UnityEvent();
        temp.AddListener(() =>
        {
            Debug.Log("Test");
        });
        OpenButtons(
            new SelectButtonData[]
            {
                new SelectButtonData("test", "test", temp),
                new SelectButtonData("test", "test", temp),
                new SelectButtonData("test", "test", temp)
            }
        );
    }

    public void OpenButtons(SelectButtonData[] dataList)
    {
        if(buttonList == null)
        {
            buttonList = new List<SkillSelectButton>();
            for (int i = 0; i < dataList.Length; i++)
            {
                GameObject obj = Instantiate(skillSelectButtonPrefab, transform);
                buttonList.Add(obj.GetComponent<SkillSelectButton>());
            }
            
            for (int i = 0; i < dataList.Length; i++)
            {
                buttonList[i].SetButton(dataList[i]);
            }
        }
        else
        {
            foreach (var temp in buttonList)
            {
                temp.gameObject.SetActive(false);
            }

            if(buttonList.Count < dataList.Length)
            {
                int diffCount = dataList.Length - buttonList.Count;
                for(int i=0; i < diffCount; i++)
                {
                    GameObject obj = Instantiate(skillSelectButtonPrefab, transform);
                    buttonList.Add(obj.GetComponent<SkillSelectButton>());
                }
            }
            for(int i = 0; i < dataList.Length; i++)
            {
                buttonList[i].SetButton(dataList[i]);
            }
            
            
        }
    }
}