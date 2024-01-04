using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;


public enum Status // <- 레벨업시 사용
{
    Health,
    Stemina, 
    Str, 
    Dex, 
    Int, 
    Lux 
}

public class PlayerStatusHandler :StatHandler
{
    private PlayerStat playerCurrentStat;
    public PlayerStat GetStat() => playerCurrentStat;  // 현재 수치 가져오기 
    public PlayerStat growStatSO;
    public PlayerStat baseStatSO;
    private PlayerAttack playerAttack;
    private Test test;

    [HideInInspector]
    public int currentHp;
    [HideInInspector]
    public float currentStemina;
    [HideInInspector]
    public int currentDamage;
    [HideInInspector]
    public int currentSpellPower;
    [HideInInspector]
    public int currentDefense;
    [HideInInspector]
    public int currentpropertyDamage;
    [HideInInspector]
    public int currentpropertyDefense;
    [HideInInspector]
    public int currentWeight;
    [HideInInspector]
    public int currentRegainHp;
    [HideInInspector]
    public int currentMana;
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float currentCritical;
    [HideInInspector]
    public float currentDelay;
    [HideInInspector]
    public float currentParryTime;
    [HideInInspector]
    public float currentSoulDrop;
    [HideInInspector]
    public float currentAttackRange;
    [HideInInspector]
    public int currentLevel;


    private void Awake()
    {
        playerCurrentStat = currentStatSO as PlayerStat; 
        growStatSO.ResetStat();
        playerCurrentStat.ResetStat();
        SetStat();
        playerAttack = GetComponent<PlayerAttack>();
        test = GetComponent<Test>();
       
    }

    
    public int CriticalCheck(int damage)
    {
        if (UnityEngine.Random.Range(0,100) < currentCritical)
        {
            int criticalDamage = damage * 2;
            return criticalDamage;
        }
        return damage;
    }

    public override void TakeDamage(int damage)
    {
        if (playerCurrentStat == null)
            return;
        if (playerAttack.isParrying)
        {
            SoundManager.instance.PlayClip(test.parrySound);
            return;
        }
        if (playerAttack.isGuarding)
            damage /= 2;
        damage = damage <= currentDefense ? 0 : damage - currentDefense;
        currentRegainHp -= damage / 2;
        currentHp -= damage;
        OnDamage?.Invoke();
    }

    public void TakeTrueDamage(int damage)
    {
        if (playerCurrentStat == null)
            return;
        currentHp -= damage;
    }
    
    protected override void SetStat()
    {
        UpdateStat();
        currentHp = playerCurrentStat.hp;
        currentStemina = playerCurrentStat.stemina;
        currentDamage = playerCurrentStat.damage;
        currentSpellPower = playerCurrentStat.spellPower;
        currentDefense = playerCurrentStat.defense;
        currentpropertyDamage = playerCurrentStat.propertyDamage;
        currentpropertyDefense = playerCurrentStat.propertyDefense;
        currentWeight = 0;
        currentRegainHp = currentHp+(currentHp/20);
        currentMana = playerCurrentStat.mana;
        currentSpeed = playerCurrentStat.speed;
        currentCritical = playerCurrentStat.criticalChance;
        currentDelay = playerCurrentStat.delay;
        currentParryTime = playerCurrentStat.parryTime;
        currentSoulDrop = playerCurrentStat.soulDropRate;
        currentAttackRange = playerCurrentStat.attackRange;
    }
    
    public void UpdateStat()
    {
        growStatSO.DetailedStat(growStatSO);
        playerCurrentStat.PlusStatToMax(baseStatSO, growStatSO);
        //playerCurrentStat.DetailedStat(playerCurrentStat);
    }
    
    public bool GrowUpStat(int num, Status status) // 레벨업 메서드
    {
        if (growStatSO == null)
            return false;
        switch (status)
        {
            case Status.Health:
                growStatSO.healthStat += num;
                break;
            case Status.Stemina:
                growStatSO.steminaStat += num;
                break;
            case Status.Str:
                growStatSO.strStat += num;
                break;
            case Status.Dex:
                growStatSO.dexStat += num;
                break;
            case Status.Int:
                growStatSO.intStat += num;
                break;
            case Status.Lux:
                growStatSO.luxStat += num;
                break;
        }
        UpdateStat();
        return true;
    }
    
    
    public void UpdateWeapon(int power, float attackSpeed, float attackRange, int weight, int propertyAmount)
    {
        currentDamage += power;
        currentDelay += attackSpeed;
        currentAttackRange += attackRange;
        currentWeight += weight;
        currentpropertyDamage += propertyAmount;
    }
    public void UpdateArmor(int power, int weight, int propertyAmount)
    {
        currentDefense += power;
        currentWeight += weight;
        currentpropertyDefense += propertyAmount;
    }

    public void FullCondition()
    {
        currentHp = playerCurrentStat.hp;
        currentStemina = playerCurrentStat.stemina;
        currentMana = playerCurrentStat.mana;
        currentRegainHp = currentHp;
    }
}
