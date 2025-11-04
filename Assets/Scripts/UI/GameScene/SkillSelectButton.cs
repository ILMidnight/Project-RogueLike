using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static StatPoint;

public class SkillSelectButton : MonoBehaviour
{
    [SerializeField]
    TMP_Text titleText;
    [SerializeField]
    TMP_Text detailText;
    [SerializeField]
    EventTrigger trigger;

    SelectButtonData currentData;

    public void SetButton(SelectButtonData initData)
    {
        currentData = initData;
        
        titleText.text = currentData.title;
        detailText.text = currentData.detail;

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) =>
        {
            currentData.selectEvent.Invoke();
        });

        trigger.triggers.Clear();

        trigger.triggers.Add(entry);

        gameObject.SetActive(true);
    }
}
