using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFlakeController : PositionAttackController
{
    [SerializeField] private PositionAttackData poisonData;
    [SerializeField] private PositionAttackData explosionData;
    private PositionAttack positionAttack;

    private void Awake()
    {
        positionAttack = GetComponent<PositionAttack>();
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackData.target.value == (attackData.target.value | (1 << collision.gameObject.layer)))
        {
            SoundManager.instance.PlayClip(soundData);
            Vector2 position = transform.position;
            positionAttack.CreateProjectile(position + Vector2.up / 2, explosionData);
            positionAttack.CreateProjectile(position + Vector2.down / 2, poisonData);
            DestroyProjectile();
        }
    }
}
