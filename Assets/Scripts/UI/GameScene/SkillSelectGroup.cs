using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static StatPoint;

public class SkillSelectGroup : MonoBehaviour
{
    [SerializeField]
    GameObject skillSelectButtonPrefab;

    List<SkillSelectButton> buttonList;

    [SerializeField]
    public PlayerManager pMng;

    public SkillUpFunctions skillFunctions;

    public PlayerStatusController pStatusController;

    public void CustomAwake()
    {
        pStatusController = pMng.states["StatusController"] as PlayerStatusController;
        skillFunctions = new SkillUpFunctions(this);

        pMng.levelUpEvent.AddListener(() =>
        {
            LevelUpEvent();
        });
    }
    public void LevelUpEvent()
    {
        OpenButtons(
            new SelectButtonData[]
            {
                skillFunctions.MaxHPChange(Random.Range(-20, 21)),
                skillFunctions.MaxHPChange(30),
                skillFunctions.MaxHPChange(-20)
            }
        );
    }

    public void OpenButtons(SelectButtonData[] dataList)
    {
        pMng.SetPause(true);

        if (buttonList == null)
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

            if (buttonList.Count < dataList.Length)
            {
                int diffCount = dataList.Length - buttonList.Count;
                for (int i = 0; i < diffCount; i++)
                {
                    GameObject obj = Instantiate(skillSelectButtonPrefab, transform);
                    buttonList.Add(obj.GetComponent<SkillSelectButton>());
                }
            }
            for (int i = 0; i < dataList.Length; i++)
            {
                buttonList[i].SetButton(dataList[i]);
            }
        }
    }

    public void CloseButtons()
    {
        pMng.SetPause(false);

        foreach (var temp in buttonList)
        {
            temp.gameObject.SetActive(false);
        }
    }
}