using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_NightBorn : EnemyCharacter
{
    [Header("Unique Setting")]
    [SerializeField] private Boss_NightBornUniqueStat uniqueStats;
    [SerializeField] private GameObject backLight;
    [SerializeField] private LayerMask wallLayer;
    private PositionAttack positionAttack;
    private bool isRage = false;

    public static List<GameObject> spiritList = new();

    protected override void Awake()
    {
        base.Awake();
        GameManager.instance.PlayerDeath += ResetDarkSpirit;
        positionAttack = GetComponent<PositionAttack>();
        statusHandler.OnRage += OnRage;
        backLight.SetActive(false);
        
        
        #region CloseRangedPattern
        pattern.AddPattern(Distance.CloseRange, Slash);
        pattern.AddPattern(Distance.CloseRange, ForwardDashSlash);
        pattern.AddPattern(Distance.CloseRange, BlinkExplosion);
        pattern.AddPattern(Distance.CloseRange, SpwanMonster);
        #endregion

        #region MediumRangePattern
        pattern.AddPattern(Distance.MediumRange, ForwardDashSlash);
        pattern.AddPattern(Distance.MediumRange, StraightExplosion);
        pattern.AddPattern(Distance.MediumRange, BlinkExplosion);
        pattern.AddPattern(Distance.MediumRange, SpwanMonster);
        pattern.AddPattern(Distance.MediumRange, Run);
        #endregion
    }
    
    protected override void Start()
    {
        base.Start();
        GameManager.instance.PlayerDeath += OffRage;
        foreach (ObjectPool.Pool projectile in uniqueStats.projectiles)
        {
            ProjectileManager.instance.InsertObjectPool(projectile);
        }
        int layer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(layer,layer, true);
    }

    protected override void SetPatternDistance()
    {
        float distance = Mathf.Abs(targetTransform.position.x - transform.position.x);
        if (distance < characterStat.closeRange)
        {
            pattern.SetDistance(Distance.CloseRange);
        }
        else
        {
            pattern.SetDistance(Distance.MediumRange);
        }
    }
    
    protected override void DetectPlayer()
    {
        targetTransform = GameManager.instance.player.transform;
        detected = true;
    }

    private void OnRage()
    {
        isRage = true;
    }

    private void OffRage()
    {
        isRage = false;
    }

    private void MeleeAttack()
    {
        soundManager.PlayClip(uniqueStats.slashSound);
        Collider2D collision = Physics2D.OverlapBox(
            attackPosition.position, uniqueStats.meleeAttackRange, 0, characterStat.target);
        if (collision != null)
        {
            // 데미지 주기
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(characterStat.damage);
        }
    }

    private IEnumerator Run()
    {
        RunningPattern();
        soundManager.PlayLoopClip(uniqueStats.runSound, uniqueStats.runSound.GetInstanceID());
        anim.HashBool(anim.run, true);
        float distance = float.MaxValue;
        while (Mathf.Abs(distance) > characterStat.closeRange)
        {
            distance = targetTransform.position.x - transform.position.x;
            rigid.velocity = GetDirection() * characterStat.speed;
            yield return YieldCache.WaitForFixedUpdate;
        }
        soundManager.StopLoopClip(uniqueStats.runSound.GetInstanceID());
        anim.HashBool(anim.run, false);
        rigid.velocity = Vector2.zero;
        state = State.FAILURE;
    }
    
    private IEnumerator Slash()
    {
        RunningPattern();
        float ran = Random.Range(0, 10);
        anim.StringTrigger("Slash");
        yield return YieldCache.WaitForSeconds(0.7f); // 애니 싱크
        MeleeAttack();
        if (ran < 5)
        {
            anim.StringTrigger("TwiceSlash");
            yield return YieldCache.WaitForSeconds(1f); // 애니 싱크
            MeleeAttack();
        }
        state = State.SUCCESS;
    }

    private IEnumerator ForwardDashSlash()
    {
        RunningPattern();
        anim.StringTrigger("ForwardDashSlash");
        Vector2 direction = GetDirection();
        yield return YieldCache.WaitForSeconds(1f);
        float moveDistance;
        bool hit = false;
        Vector2 startPosition = transform.position;
        soundManager.PlayClip(uniqueStats.slashSound);
        do 
        {
            Vector3 position = transform.position;
            moveDistance = position.x - startPosition.x;
            rigid.velocity = direction * uniqueStats.fowardDashSlashSpeed;
            if (!hit)
            {
                Collider2D collision = Physics2D.OverlapBox(
                    attackPosition.position, uniqueStats.meleeAttackRange, 0, characterStat.target);
                if (collision != null)
                {
                    // 데미지 주기
                    hit = true;
                    collision.GetComponent<PlayerStatusHandler>().TakeDamage(characterStat.damage);
                }
            }
            yield return YieldCache.WaitForFixedUpdate;
        }
        while (Mathf.Abs(moveDistance) < uniqueStats.fowardDashSlashDistance && !CheckTile(direction));
        rigid.velocity = Vector2.zero;
        state = State.SUCCESS;
    }

    private IEnumerator StraightExplosion()
    {
        RunningPattern();
        anim.StringTrigger("OnStraightExplosion");
        yield return YieldCache.WaitForSeconds(1f); // 애니 싱크
        if (GetDirection().x < 0)
        {
            Vector3 position = new Vector2(uniqueStats.minX, transform.position.y + 1f);
            positionAttack.CreateMultipleProjectile( position, uniqueStats.bornExplosion, false);
        }
        else
        {
            Vector3 position = new Vector2(uniqueStats.maxX, transform.position.y + 1f);
            positionAttack.CreateMultipleProjectile(position, uniqueStats.bornExplosion, true);
        }
        yield return YieldCache.WaitForSeconds(2f); // 패턴끝나는거 기다리기
        anim.StringTrigger("EndStraightExplosion");
        yield return YieldCache.WaitForSeconds(0.5f);
        state = State.SUCCESS;
    }

    private IEnumerator BlinkExplosion()
    {
        RunningPattern();
        soundManager.PlayClip(uniqueStats.warpSound);
        anim.StringTrigger("OnBlinkExplosion");
        yield return YieldCache.WaitForSeconds(0.7f); // 애니 싱크
        Vector3 position = transform.position += (targetTransform.position.x - transform.position.x) * Vector3.right;
        soundManager.PlayClip(uniqueStats.warpSound);
        yield return YieldCache.WaitForSeconds(1f); // 애니 싱크
        positionAttack.CreateBothSideProjectile(position + Vector3.up,
            uniqueStats.bothSideExplosion);
        yield return YieldCache.WaitForSeconds(1f); // 애니 싱크
        anim.StringTrigger("EndBlinkExplosion");
        state = State.SUCCESS;
    }

    private IEnumerator SpwanMonster()
    {
        RunningPattern();   
        if (!isRage || spiritList.Count >= 5)
        {
            state = State.FAILURE;
            yield break;
        }
        soundManager.PlayClip(uniqueStats.roarSound);
        anim.StringTrigger("SpwanMonster");
        yield return YieldCache.WaitForSeconds(0.5f); // 애니 싱크
        float ran = Random.Range(uniqueStats.minX, uniqueStats.maxX);
        Vector2 position = new Vector2(ran, transform.position.y + 1f);
        positionAttack.CreateProjectile(position, uniqueStats.spwanBall);
        yield return YieldCache.WaitForSeconds(0.5f); // 애니 싱크
        state = State.SUCCESS;
    }
    
    private bool CheckTile(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, dir, 2f, wallLayer);
        if (hit.collider != null)
            return true;
        return false;
    }

    private void ResetDarkSpirit()
    {
        for (int i = 0; i < spiritList.Count; i++)
        {
            Destroy(spiritList[i]);
        }
        spiritList.Clear();
        int layer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(layer, layer, false);
    }

    protected override void Death()
    {
        base.Death();
        foreach (ObjectPool.Pool projectile in uniqueStats.projectiles)
        {
            ProjectileManager.instance.DeleteObjectPool(projectile.tag);
        }
        ResetDarkSpirit();
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
