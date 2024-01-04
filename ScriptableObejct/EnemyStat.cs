using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "Stats/EnemyStat/DefaultStat", order = 2)]
public class EnemyStat : BaseStat
{
   [Header("Enemy Stats")] 
   public float detectRange;
   public LayerMask target;
   
   [Header("Distance Setting")]
   public float closeRange;
   public float mediumRange;
   public float longRange;
}
