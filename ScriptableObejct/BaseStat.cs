using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseStat", menuName = "Stats/BaseStat", order = 0)]
public class BaseStat : ScriptableObject
{
   [Header("Base Stats")]
   public int hp;
   public int damage;
   public int defense;
   public float speed;
   public float delay; // 플레이어 공격속도 = 1f
   public float attackRange; // 플레이어 공격 범위 = 1f
   public int propertyDamage;
   public int propertyDefense;

   public void CopyStat(BaseStat stat)
   {
      hp = stat.hp;
      damage = stat.damage;
      defense = stat.defense;
      speed = stat.speed;
      delay = stat.delay;
      attackRange = stat.attackRange;
      propertyDamage = stat.propertyDamage;
      propertyDefense = stat.propertyDefense;
   }
}
