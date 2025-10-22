using System;
using UnityEngine;

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
    public float Hp;
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



    public StatPoint(float Hp, float shield, float movementSpeed, float attackRange, int coolDownSpeed, int strength)
    {
        this.Hp = Hp;
        this.shield = shield;

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

        return new StatPoint
        (
            lerpHp,
            lerpShield,
            lerpMoveSpeed,
            lerpAttackRange,
            lerpCoolDownSpeed,
            lerpStrength
        );
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