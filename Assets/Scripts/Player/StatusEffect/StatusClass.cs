using System;
using UnityEngine;
using UnityEngine.Events;

public enum Buff
{
    Shield = 0,
    DamageBoost,
    SkllBoost,
    ArmorBoost,
    HitRangeUp,
    Petrify
}

public enum Debuff
{
    Burn = 0,
    Poison,
    Bind,
    Freeze,
    stun,
    Knockback
}

[Serializable]
public struct StatPoint
{
    // HP 공식 : 기본 HP(레벨별 체력) + maxHP
    public float MaxHP;
    // 현재 체력
    public float Hp;
    // 기본 데미지 수치, 각 무기별로 데미지 공식 계산
    public float Damage;
    // 기본 히트스캔 공격 기준 1 / AttackSpeed  공식을 이용하여 초당 몇번 공격하는 지 계산 하여 적용
    public float AttackSpeed;
    public int RegenerationHP;
    public float shield;
    // 기본 속도에 더하기
    public float movementSpeed;
    // 총탄류는 데미지가 감소되기 시작하는 거리가 늘어남
    // 타겟팅은 타겟팅이 되는 범위가 증가
    // 범위공격은 작동 범위가 증가
    public float attackRange;
    // 기본 재사용 대기시간에서 퍼센트로 감소
    public int coolDownSpeed;
    // 상태이상 지속 시간 퍼센트로 감소
    public int strength;

    #region ControllStatus Functions
    public struct SelectButtonData
{
    public SelectButtonData(string t, string d, UnityEvent s)
    {
        title = t;
        detail = d;
        selectEvent = s;
    }
    public string title;
    public string detail;

    public UnityEvent selectEvent;
}

public class SkillUpFunctions
{
    public SkillUpFunctions(SkillSelectGroup skillGroup)
    {
        this.skillGroup = skillGroup;
    }
    SkillSelectGroup skillGroup;
    public SelectButtonData MaxHPChange(int value)
    {
        var temp = new SelectButtonData();
        temp.title = "Max HP " + (value > 0 ? "Up" : "Down");
        temp.detail = "Max HP " + Mathf.Abs(value) + (value > 0 ? " Up" : " Down");
        var tempEvent = new UnityEvent();
        tempEvent.AddListener(() =>
        {
            Debug.Log("Test");
            skillGroup.pStatusController.ChangeMaxHp(value);
            skillGroup.CloseButtons();
            Debug.Log(skillGroup.pStatusController.baseStatus.MaxHP);
        });
        temp.selectEvent = tempEvent;
        return temp;
    }

    // public Sele
}
    #endregion

    public StatPoint(float Hp, float shield, float movementSpeed, float attackRange, int coolDownSpeed, int strength)
    {
        this.MaxHP = this.Hp = Hp;
        this.shield = shield;

        this.Damage = 10;
        this.AttackSpeed = 5;

        this.RegenerationHP = 10;
        this.movementSpeed = movementSpeed;
        this.attackRange = attackRange;
        this.coolDownSpeed = coolDownSpeed;
        this.strength = strength;
    }
    
    public static StatPoint Lerp(StatPoint start, StatPoint end, float t)
    {
        t = Mathf.Clamp01(t);

        float lerpHp = (start.Hp + (end.Hp - start.Hp) * t);
        float lerpShield = (start.shield + (end.shield - start.shield) * t);
        float lerpMoveSpeed = start.movementSpeed + (end.movementSpeed - start.movementSpeed) * t;
        float lerpAttackRange = start.attackRange + (end.attackRange - start.attackRange) * t;
        int lerpCoolDownSpeed = (int)Mathf.Round(start.coolDownSpeed + (end.coolDownSpeed - start.coolDownSpeed) * t);
        int lerpStrength = (int)Mathf.Round(start.strength + (end.strength - start.strength) * t);

        if (Mathf.Abs(end.Hp - lerpHp) < .1f)
            lerpHp = end.Hp;
        if (Mathf.Abs(end.shield - lerpShield) < .1f)
            lerpShield = end.shield;

        var temp = new StatPoint
        (
            lerpHp,
            lerpShield,
            lerpMoveSpeed,
            lerpAttackRange,
            lerpCoolDownSpeed,
            lerpStrength
        );

        temp.MaxHP = end.MaxHP;
        return temp;
    }
}

public class ShieldBuff : BaseStatusEffect
{
    public ShieldBuff(PlayerStatusController pState, int startShield) : base(pState)
    {
        pState.baseStatus.shield = startShield;
    }

    public override void Tick()
    {
        base.Tick();

        pState.baseStatus.shield = Mathf.Max(0, pState.baseStatus.shield - (Time.deltaTime * .1f));

        if (pState.baseStatus.shield <= 0)
        {
            EndEffect();
        }
    }

    public override void EndEffect()
    {
        base.EndEffect();
    }

    public override void StartEffect()
    {
        base.StartEffect();
    }
}

public class BurnDeBuff : BaseStatusEffect
{
    float burnDamage;

    public BurnDeBuff(PlayerStatusController pState, int burnDamage) : base(pState)
    {
        this.burnDamage = burnDamage;
    }
}


public interface IStatusEffect
{
    public void StartEffect();
    public void EndEffect();
    public void Tick();
}

public class BaseStatusEffect : IStatusEffect
{
    protected PlayerStatusController pState;

    public BaseStatusEffect(PlayerStatusController pState)
    {
        this.pState = pState;

        StartEffect();
    }

    public virtual void StartEffect()
    {
        Debug.Log($"{GetType().ToString()} to Started");
    }
    
    public virtual void EndEffect()
    {
        Debug.Log($"{GetType().ToString()} to End");

        pState.RemoveEffect(this);
    }

    public virtual void Tick()
    {
        
    }
}