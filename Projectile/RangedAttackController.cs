using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] protected LayerMask baseCollision;
    [SerializeField] protected AudioClip soundData;
    [SerializeField] private bool playStartSound;
    protected RangedAttackData attackData;
    protected Vector2 direction;
    private float currentDuration;
    private bool isReady;

    protected Rigidbody2D rigid;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (playStartSound)
            SoundManager.instance.PlayClip(soundData);
    }
    
    protected virtual void Update()
    {
        if (!isReady)
            return;
        currentDuration += Time.deltaTime;

        if (currentDuration > attackData.duration) {
            DestroyProjectile(transform.position);
        }
        rigid.velocity = direction * attackData.speed;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (baseCollision.value == (baseCollision.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f);
        }
        else if (attackData.target.value == (attackData.target.value | (1 << collision.gameObject.layer)))
        {
            // 데미지 주기
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(attackData.damage);
            DestroyProjectile(transform.position);
        }
    }

    public virtual void InitializeAttack(Vector2 direction, RangedAttackData attackData)
    {
        this.attackData = attackData;
        this.direction = direction;
        currentDuration = 0;
        transform.right = direction;
        isReady = true;
    }
    protected void DestroyProjectile(Vector2 position)
    {
        //파티클
        gameObject.SetActive(false);
    }
}
