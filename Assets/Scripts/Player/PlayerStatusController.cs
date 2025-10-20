using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatusController : PlayerControllerBase
{
    #region Status Values
    public StatPoint baseStatus;
    public StatPoint displayStatus;

    List<BaseStatusEffect> effectList;
    #endregion

    PlayerUIController pUIController;

    TMP_Text hpText;
    TMP_Text shieldText;

    public PlayerStatusController(PlayerManager pMng) : base(pMng)
    {
        InItPlayer();

        pUIController = pMng.states["PlayerUIController"] as PlayerUIController;
    }

    void InItPlayer()
    {
        baseStatus = new StatPoint(
            100, 0, 7, 30, 0, 0
        );
        displayStatus = new StatPoint(0, 0, 0, 0, 0, 0);

        effectList = new List<BaseStatusEffect>();

        AddBuff(Buff.Shield);
    }

    void SetUIText()
    {
        if (pUIController.statUI)
        {
            hpText = pUIController.statUI.transform.GetChild(0).GetComponent<TMP_Text>();
            shieldText = pUIController.statUI.transform.GetChild(1).GetComponent<TMP_Text>();
        }
    }

    public override void Tick()
    {
        base.Tick();

        for(int i = effectList.Count -1; i >= 0; i--)
        {
            effectList[i].Tick();
        }
        
        displayStatus = StatPoint.Lerp(displayStatus, baseStatus, Time.deltaTime * 2.5f);

        if(hpText == null)
        {
            SetUIText();
        }
        else
        {
            hpText.text = ((int)(Mathf.Max(displayStatus.Hp, 0))).ToString();
            shieldText.text = ((int)(Mathf.Max(displayStatus.shield, 0))).ToString();
        }
    }

    public void RemoveEffect(BaseStatusEffect effect)
    {
        effectList.Remove(effect);
    }
    
    public void AddDamage(int damage)
    {
        if(baseStatus.shield > 0)
        {
            if(baseStatus.shield < damage)
            {
                baseStatus.Hp -= (damage - baseStatus.shield);
                baseStatus.shield = 0;
            }
            else
            {
                baseStatus.shield -= damage;
            }
        }
        else
        {
            baseStatus.Hp -= damage;
        }
    }

    public void AddBuff(Buff buff)
    {
        switch (buff)
        {
            case Buff.Shield:
                effectList.Add(new ShieldBuff(this, 100));
                break;
        }
    }
}
