using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUniqueStats", menuName = "Stats/EnemyStat/ArcherUniqueStats", order = 5)]
public class ArcherUniqueStat : ScriptableObject
{
    [Header("Boss_DB Stats")]
    public Vector2 meleeAttackRange;

    [Header("meleeAttack")]


    [Header("UseSpell")]
    public RangedAttackData BA_Arrow;

    [Header("Projectile")]
    public List<ObjectPool.Pool> projectiles;

    [Header("Sound")]
    public AudioClip runSound;
    public AudioClip meleeAttackSound;
}