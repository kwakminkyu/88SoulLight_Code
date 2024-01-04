using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="RangedAttackData", menuName = "AttackData/RangedAttackData", order = 0)]
public class RangedAttackData : ScriptableObject
{
    [Header("Ranged Attack Data")] 
    public string tag;
    public int damage;
    public int speed;
    public float duration;
    public float spread;
    public LayerMask target;
    
    [Header("Multiple Attack Data")] 
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngle;
}
