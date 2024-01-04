using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScatterArrowController : RangedAttackController
{
    [SerializeField] private RangedAttackData smallArrowData;
    [SerializeField] private Vector2 rayBoxSize;
    private RangedAttack rangedAttack;
    
    protected override void Awake()
    {
        base.Awake();
        rangedAttack = GetComponent<RangedAttack>();
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + direction,
            rayBoxSize, Mathf.Atan2(direction.y, direction.x), direction, 0.1f, baseCollision.value);
        if (baseCollision.value == (baseCollision.value | (1 << collision.gameObject.layer)))
        {
            SoundManager.instance.PlayClip(soundData);
            rangedAttack.CreateMultipleProjectile(Vector2.Reflect(direction, hit.normal), smallArrowData);
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f);
        }
        else if (attackData.target.value == (attackData.target.value | (1 << collision.gameObject.layer)))
        {
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(attackData.damage);
            DestroyProjectile(transform.position);
        }
    }
}
