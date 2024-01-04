using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss_HawkeyeUniqueStats", menuName = "Stats/EnemyStat/Boss_HawkeyeUniqueStats", order = 2)]
public class Boss_HawkeyeUniqueStat : ScriptableObject
{
    [Header("Boss_Hawkeye Stats")] 
    public Vector2 meleeAttackRange;
    
    [Header("DodgeAttack")]
    public float dodgeDistance;
    public float dodgeTime;
    public float secondAttackDistance;
    public float secondAttackTime;
    
    [Header("BackstepAttack")]
    public float backstepDistance;
    public float backstepTime;
    public float backstepJumpPosition;
    
    [Header("TrackingAttack")]
    public float trackingDistance;
    public float trackingSpeed;
    public int numberOfTrackingAttacks;
    public float trackingAttacksWaitTime;
    
    [Header("BackTumbling")] 
    public float backTumblingDistance;
    public float backTumblingTime;

    [Header("SpinDashAttack")] 
    public float spinDashAttackDistance;
    public float spinDashAttackSpeed;
    
    [Header("Leap")] 
    public float leapPosition;
    public float leapTime;

    [Header("LeapShot")] 
    public int numberOfLeapShot;
    public float aimingTime;
    
    [Header("Projectile")] 
    public List<ObjectPool.Pool> projectiles;
    
    [Header("ArrowData")]
    public RangedAttackData arrowData;
    public RangedAttackData bombArrowData;
    public RangedAttackData poisonArrowData;
    public RangedAttackData scatterArrowData;
    public PositionAttackData poisonFlaskData;
    
    [Header("Sound")]
    public AudioClip runSound;
    public AudioClip dodgeSound;
    public AudioClip jumpSound;
    public AudioClip meleeAttackSound;
    public AudioClip arrowAttackSound;
    public AudioClip ScatterArrowSound;
    public AudioClip spinAttackSound;
}
