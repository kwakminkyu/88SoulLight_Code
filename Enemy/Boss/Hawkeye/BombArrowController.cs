using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrowController : RangedAttackController
{
    [SerializeField] private PositionAttackData bombExplosionData;
    [SerializeField] private Vector2 rayBoxSize;
    [SerializeField] private float extinctionTime;
    private float lifeTime;
    
    private PositionAttack positionAttack;
    private bool isHit;

    protected override void Awake()
    {
        base.Awake();
        positionAttack = GetComponent<PositionAttack>();
    }

    protected override void Update()
    {
        if (isHit)
        {
            rigid.velocity = Vector2.zero;
            lifeTime += Time.deltaTime;
            if (lifeTime > extinctionTime)
                ResetObject();
        }
        else
        {
            base.Update();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + direction,
            rayBoxSize, Mathf.Atan2(direction.y, direction.x), direction, 0.1f, baseCollision.value);
        if (baseCollision.value == (baseCollision.value | (1 << collision.gameObject.layer)))
        {
            Vector3 position = transform.position;
            if (1 << LayerMask.NameToLayer("Ground") == 1 << collision.gameObject.layer)
            {
                positionAttack.CreateBothSideProjectile(position, bombExplosionData);
            }
            else
            {
                positionAttack.CreateProjectile(position, bombExplosionData);

            }
            isHit = true;
        }
        else if (attackData.target.value == (attackData.target.value | (1 << collision.gameObject.layer)))
        {
            // 데미지 주기
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(attackData.damage);
            DestroyProjectile(transform.position);
        }
    }

    private void ResetObject()
    {
        isHit = false;
        lifeTime = 0f;
        DestroyProjectile(transform.position);
    }
}
