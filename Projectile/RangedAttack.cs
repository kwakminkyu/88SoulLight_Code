using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangedAttack : MonoBehaviour
{
    private ProjectileManager projectileManager;
    [SerializeField] private Transform attackPosition;
    
    private void Start()
    {
        projectileManager = ProjectileManager.instance;
    }

    public void CreateProjectile(Vector2 dir, RangedAttackData data)
    {
        projectileManager.ActiveProjectile(attackPosition.position, dir, data);
    }
    
    public void CreateMultipleProjectile(Vector2 dir, RangedAttackData data)
    {
        // 각도 아래로 내림
        float shotAngle = data.multipleProjectilesAngle;
        int shotNum = data.numberofProjectilesPerShot;
        float minAngle = -(shotNum / 2f) * shotAngle + 0.5f * shotAngle;
        for (int i = 0; i < shotNum; i++)
        {
            float angle = minAngle + shotAngle * i;
            float randomSpread = Random.Range(-data.spread, data.spread);
            angle += randomSpread;
            projectileManager.ActiveProjectile(
                attackPosition.position,
                RotateVector2(dir, angle),
                data
            );
        }
    }
    
    private Vector2 RotateVector2(Vector2 v, float degree) {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
