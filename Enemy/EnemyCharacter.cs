using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyCharacter : MonoBehaviour
{
    protected enum State
    {
        SUCCESS,
        RUNNING,
        FAILURE
    }
    
    [Header("Base Setting")]
    [Space(10)]
    [SerializeField] protected Transform attackPosition;
    private Vector3 characterPosition;
    protected EnemyStat characterStat;
    protected Transform targetTransform;
    protected EnemyStatusHandler statusHandler;
    protected EnemyAnimationController anim;
    protected EnemyPattern pattern;
    protected Rigidbody2D rigid;
    protected SoundManager soundManager;

    private Coroutine currentPattern;
    private float currentTime = float.MaxValue;
    protected State state;
    protected bool detected;

    protected virtual void Awake()
    {
        statusHandler = GetComponent<EnemyStatusHandler>();
        anim = GetComponent<EnemyAnimationController>();
        pattern = GetComponent<EnemyPattern>();
        rigid = GetComponent<Rigidbody2D>();
        characterPosition = transform.position;
        statusHandler.OnDeath += Death;
    }

    protected virtual void Start()
    {
        GameManager.instance.PlayerDeath += ResetEnemy;
        soundManager = SoundManager.instance;
        characterStat = statusHandler.GetStat();
    }

    private void Update()
    {
        if (!detected)
        {
            DetectPlayer();
        }
        else
        {
            if (state == State.FAILURE)
                ActionPattern();
            if (state != State.RUNNING)
                currentTime += Time.deltaTime;
            if (currentTime > characterStat.delay)
            {
                ActionPattern();
            }
        }
    }
    
    private void ActionPattern()
    {
        currentTime = 0f;
        if (currentPattern != null)
            StopCoroutine(currentPattern);
        SetPatternDistance();
        currentPattern = StartCoroutine(pattern.GetPattern()());
    }

    protected abstract void SetPatternDistance();

    protected virtual void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(((Vector2)transform.position + Vector2.up) + (Vector2.right * characterStat.detectRange),
            Vector2.left, characterStat.detectRange * 2, characterStat.target);
        if (hit.collider != null)
        {
            targetTransform = GameManager.instance.player.transform;
            detected = true;
        }
    }

    protected void RunningPattern()
    {
        state = State.RUNNING;
        Rotate();
    }

    private void ResetEnemy()
    {
        StopEnemy();
        detected = false;
        transform.position = characterPosition;
        statusHandler.ResetHealth();
    }
    
    protected virtual void Rotate()
    {
        transform.rotation = targetTransform.position.x < transform.position.x
            ? Quaternion.Euler(0, 180, 0)
            : Quaternion.Euler(0, 0, 0);
    } 
    
    protected Vector2 GetDirection()
    {
        return targetTransform.position.x - transform.position.x < 0 ? Vector2.left : Vector2.right;
    }

    protected virtual void Death()
    {
        StopEnemy();
        anim.HashTrigger(anim.death);
    }

    private void StopEnemy()
    {
        state = State.RUNNING;
        if (currentPattern != null)
            StopCoroutine(currentPattern);
        rigid.velocity = Vector2.zero;
        currentTime = 0f;
    } 
}
