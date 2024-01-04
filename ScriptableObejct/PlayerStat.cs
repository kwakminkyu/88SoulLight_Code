using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Stats/PlayerStat", order = 1)]
public class PlayerStat : BaseStat
{
    [Header("Level Info")]
    public int level;
    public int levelPoint;
    
    [Header("Grow Stats")]
    public int healthStat;
    public int steminaStat;
    public int strStat;
    public int dexStat;
    public int intStat;
    public int luxStat;
    
    [Header("Player Stats")]

    public int weight;
    public int spellPower; // 주문력
    public int regainHp; //재생체력
    public float stemina;
    public float invincibleTime;
    public float parryTime;
    public float extraMoveSpeed; // 1.5f
    public float soulDropRate; 
    public float criticalChance; // 크리티컬 확률
    public int mana;
    public void CopyBaseStat(PlayerStat basestat)
    {
        base.CopyStat(basestat);
        level = basestat.level;
        levelPoint = basestat.levelPoint;
        healthStat = basestat.healthStat;
        steminaStat= basestat.steminaStat;
        strStat = basestat.strStat;
        dexStat = basestat.dexStat;
        intStat = basestat.intStat;
        luxStat= basestat.luxStat;
        
        stemina = basestat.stemina;
        weight = basestat.weight;
        spellPower = basestat.spellPower;
        regainHp = basestat.regainHp;
        invincibleTime = basestat.invincibleTime;
        parryTime = basestat.parryTime;
        extraMoveSpeed = basestat.extraMoveSpeed;
        soulDropRate = basestat.soulDropRate;
        criticalChance = basestat.criticalChance;
        mana = basestat.mana;
    }
   
    public void DetailedStat(PlayerStat stat) 
    {
        
       hp = stat.healthStat * 10;
       stemina = stat.steminaStat * 5;
       defense = stat.healthStat * 1;
       stemina = stat.steminaStat * 5; 
       weight = stat.steminaStat * 3;
       parryTime = stat.strStat * 0.01f;
       invincibleTime = stat.dexStat * 0.01f;
       spellPower = stat.intStat * 1; // 수치 수정 필요
       propertyDamage = stat.intStat * 1; // 수치 수정 필요
       criticalChance = stat.luxStat * 0.1f;
       soulDropRate = stat.luxStat * 10f;
       
       damage = stat.strStat * 4 + stat.dexStat * 2; 
    }

    public void PlusStatToMax(PlayerStat baseStat, PlayerStat growStat)
    {
            hp = baseStat.hp + growStat.hp;
            damage = baseStat.damage + growStat.damage;
            defense = baseStat.defense + growStat.defense;
            speed = baseStat.speed + growStat.speed;
            delay = baseStat.delay + growStat.delay;
            attackRange = baseStat.attackRange + growStat.attackRange;
            propertyDamage = baseStat.propertyDamage + growStat.propertyDamage;
            propertyDefense = baseStat.propertyDefense + growStat.propertyDefense;
            
            level = baseStat.level + growStat.level;
            levelPoint = baseStat.levelPoint + growStat.levelPoint;
            
            healthStat = baseStat.healthStat + growStat.healthStat;
            steminaStat = baseStat.steminaStat + growStat.steminaStat;
            strStat = baseStat.strStat + growStat.strStat;
            dexStat = baseStat.dexStat + growStat.dexStat;
            intStat = baseStat.intStat + growStat.intStat;
            luxStat = baseStat.luxStat + growStat.luxStat;
            
            stemina = baseStat.stemina + growStat.stemina;
            weight = baseStat.weight + growStat.weight;
            spellPower = baseStat.spellPower + growStat.spellPower;
            regainHp = baseStat.regainHp + growStat.regainHp;
            invincibleTime = baseStat.invincibleTime + growStat.invincibleTime;
            parryTime = baseStat.parryTime + growStat.parryTime;
            extraMoveSpeed = baseStat.extraMoveSpeed + growStat.extraMoveSpeed;
            soulDropRate = baseStat.soulDropRate + growStat.soulDropRate;
            criticalChance = baseStat.criticalChance + growStat.criticalChance;
            mana = baseStat.mana + growStat.mana;
    }

    public void ResetStat()
    {
        healthStat = 0;
        steminaStat = 0;
        strStat = 0;
        dexStat = 0;
        intStat = 0;
        luxStat = 0;
    }

}


