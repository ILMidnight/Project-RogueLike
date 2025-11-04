using System.Collections;
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
    PlayerMovementController pMovementController;

    TMP_Text hpText;
    TMP_Text shieldText;

    bool _nowRegenerationHP;
    bool nowRegenerationHP
    {
        get
        {
            return _nowRegenerationHP;
        }
        set
        {
            _nowRegenerationHP = value;
            if (value)
                pMng.RegenerationHP();
        }
    }
    int delayRegenerationTime = 2;

    public PlayerStatusController(PlayerManager pMng) : base(pMng)
    {
        // InitController();
    }

    public override void InitController()
    {
        pUIController = pMng.states["UIController"] as PlayerUIController;
        pMovementController = pMng.states["MoveController"] as PlayerMovementController;

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

        if (hpText == null)
        {
            SetUIText();
        }
        else
        {
            hpText.text = $"{(int)(Mathf.Max(displayStatus.Hp, 0))} / {(int)(Mathf.Max(displayStatus.MaxHP, 0))}";
            shieldText.text = ((int)(Mathf.Max(displayStatus.shield, 0))).ToString();
        }
    }

    public void RemoveEffect(BaseStatusEffect effect)
    {
        effectList.Remove(effect);
    }

    public IEnumerator RegenerationHP()
    {
        while (nowRegenerationHP)
        {
            ChangeHp(1);
            yield return new WaitForSeconds(60 / baseStatus.RegenerationHP);
        }
    }
    
    public IEnumerator DelayRegenerationHP()
    {
        nowRegenerationHP = false;
        yield return new WaitForSeconds(delayRegenerationTime);
        nowRegenerationHP = true;
    }

    public void ChangeHp(float value)
    {
        baseStatus.Hp = Mathf.Clamp(baseStatus.Hp + value, 0, baseStatus.MaxHP);
    }
    public void ChangeMaxHp(float value)
    {
        baseStatus.MaxHP = Mathf.Max(0, baseStatus.MaxHP + value);



        if (baseStatus.Hp > baseStatus.MaxHP)
            baseStatus.Hp = baseStatus.MaxHP;
    }

    public void AddDamage(int damage)
    {
        if (baseStatus.shield > 0)
        {
            if (baseStatus.shield < damage)
            {
                ChangeHp(damage - baseStatus.shield);
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

        baseStatus.Hp = Mathf.Clamp(baseStatus.Hp, 0, baseStatus.MaxHP);
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
