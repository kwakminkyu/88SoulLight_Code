using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingEye : EnemyCharacter
{
    [SerializeField] private Vector2 meleeAttackRange;
    [SerializeField] private Vector2 detectBox;
    [SerializeField] private float rollingAttackTime;
    [SerializeField] private GameObject effect;
    [SerializeField] private LayerMask ignoreLayer;

    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip rollingAttackSound;
    private Collider2D coll;
    
    private bool closed;

     protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<Collider2D>();
        effect.SetActive(false);
        
        #region Pattern
        pattern.AddPattern(Distance.CloseRange, Attack);
        pattern.AddPattern(Distance.CloseRange, RollingAttack);
        pattern.AddPattern(Distance.Default, Run);
        #endregion
    }
     
    protected override void SetPatternDistance()
    {
        if (closed)
            pattern.SetDistance(Distance.CloseRange);
        else 
            pattern.SetDistance(Distance.Default);
    }

    protected override void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, detectBox, 0,
            Vector2.down, 3, characterStat.target);
        if (hit.collider != null)
        {
            targetTransform = GameManager.instance.player.transform;
            detected = true;
        }
    }
    
    private void MeleeAttack(Vector2 position)
    {
        Collider2D collision = Physics2D.OverlapBox(
            position, meleeAttackRange, 0, characterStat.target);
        if (collision != null)
        {
            // 데미지 주기
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(characterStat.damage);
        }
    }

    private IEnumerator Run()
    {
        RunningPattern();
        RaycastHit2D hit;
        while (!closed)
        {
            Rotate();
            hit = Physics2D.CircleCast(transform.position, characterStat.attackRange,
                Vector2.zero, 0f, ignoreLayer.value | characterStat.target.value);
            if (hit.collider != null)
            {
                if (1 << hit.collider.gameObject.layer == (1 << hit.collider.gameObject.layer | characterStat.target.value))
                {
                    closed = true;
                }
                Physics2D.IgnoreCollision(coll, hit.collider);
            }
            Vector3 direction = (targetTransform.position + Vector3.up * 1.2f) - transform.position;
            rigid.velocity = direction.normalized * characterStat.speed;
            yield return YieldCache.WaitForFixedUpdate;
        }
        rigid.velocity = Vector2.zero;
        state = State.FAILURE;
    }

    private IEnumerator Attack()
    {
        RunningPattern();
        Vector2 direction = GetDirection();
        anim.HashTrigger(anim.attack);
        yield return YieldCache.WaitForSeconds(0.5f);// 애니메이션 싱크
        soundManager.PlayClip(attackSound);
        MeleeAttack((Vector2)transform.position + direction);
        closed = false;
        state = State.SUCCESS;
    }
    
    private IEnumerator RollingAttack()
    {
        RunningPattern();
        anim.StringTrigger("RollingAttack");
        Vector2 direction = GetDirection();
        yield return YieldCache.WaitForSeconds(0.5f);
        soundManager.PlayClip(rollingAttackSound);
        bool hit = false;
        float elapsedTime = 0f;
        while(elapsedTime < rollingAttackTime)
        {
            elapsedTime += Time.deltaTime;
            rigid.velocity = direction * characterStat.speed;
            if (!hit)
            {
                Collider2D collision = Physics2D.OverlapBox(
                    transform.position, meleeAttackRange, 0, characterStat.target);
                if (collision != null)
                {
                    // 데미지 주기
                    hit = true;
                    collision.GetComponent<PlayerStatusHandler>().TakeDamage(characterStat.damage);
                }
            }
            yield return YieldCache.WaitForFixedUpdate;
        }
        rigid.velocity = Vector2.zero;
        closed = false;
        state = State.SUCCESS;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
