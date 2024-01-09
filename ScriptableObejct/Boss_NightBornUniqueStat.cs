using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss_NightBornUniqueStats", menuName = "Stats/EnemyStat/Boss_NightBornUniqueStats", order = 3)]
public class Boss_NightBornUniqueStat : ScriptableObject
{
    [Header("NightBorn Stats")] 
    public Vector2 meleeAttackRange;
    public float minX;
    public float maxX;

    [Header("ForwardDashSlash")] 
    public float fowardDashSlashDistance;
    public float fowardDashSlashSpeed;
    
    [Header("Projectile")] 
    public List<ObjectPool.Pool> projectiles;

    [Header("ProjectileData")] 
    public PositionAttackData bornExplosion;
    public PositionAttackData bothSideExplosion;
    public PositionAttackData spwanBall;
    
    [Header("Sound")] 
    public AudioClip runSound;
    public AudioClip slashSound;
    public AudioClip warpSound;
    public AudioClip roarSound;
}
