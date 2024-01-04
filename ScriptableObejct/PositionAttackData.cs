using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PositioningAttackData", menuName = "AttackData/PositionAttackData", order = 1)]
public class PositionAttackData : ScriptableObject
{
    [Header("Positioning Attack Data")] 
    public string tag;
    public int damage;
    public float duration;
    public LayerMask target;

    [Header("Fixed Position Attack")] 
    public Transform position;
    
    [Header("Multiple Attack Data")] 
    public int numberofPositionAttack;
    public Vector2 followUpDirection;
    public float delayTime;

    [Header("Area of Effect")] 
    public bool AoE;
    public float damageInterval;
}
