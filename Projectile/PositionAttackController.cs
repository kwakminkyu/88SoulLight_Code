using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAttackController : MonoBehaviour
{
    [SerializeField] protected AudioClip soundData;
    [SerializeField] private bool playStartSound;
    [SerializeField] private float delaySound;
    protected PositionAttackData attackData;
    private float currentDuration;
    private float currentInterval = float.MaxValue;
    private bool contactPlayer;
    private bool soundReady;
    private bool isReady;

    private void Start()
    {
        if (playStartSound && soundReady)
            SoundManager.instance.PlayClip(soundData);
    }

    private void Update()
    {
        if (!isReady)
            return;
        
        currentDuration += Time.deltaTime;

        if (contactPlayer)
            currentInterval += Time.deltaTime;

        if (soundReady && delaySound != 0 && delaySound < currentDuration)
        {
            SoundManager.instance.PlayClip(soundData);
            soundReady = false;
        }

        if (currentDuration > attackData.duration) {
            DestroyProjectile();
        }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attackData.AoE && attackData.target.value == (attackData.target.value | (1 << collision.gameObject.layer)))
        {
            // 데미지 주기
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(attackData.damage);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        contactPlayer = true;
        if (attackData.AoE && attackData.target.value == (attackData.target.value | (1 << collision.gameObject.layer)) 
            && currentInterval > attackData.damageInterval)
        {
            // 데미지 주기
            currentInterval = 0f;
            collision.GetComponent<PlayerStatusHandler>().TakeDamage(attackData.damage);
        }
    }

    public void InitializeAttack(Vector2 position, PositionAttackData attackData)
    {
        this.attackData = attackData;
        transform.position = position;
        currentDuration = 0;
        isReady = true;
        soundReady = true;
    }

    protected virtual void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
