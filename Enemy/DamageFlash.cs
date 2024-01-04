using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mainSprite;
    [SerializeField] private Material flashMaterial;
    [SerializeField] private Material originalMaterial;
    private float duration = .2f;
    private float currentTime;
    private bool hit;

    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
            GetComponent<PlayerStatusHandler>().OnDamage += Flash;
        else
            GetComponent<EnemyStatusHandler>().OnDamage += Flash;

    }

    private void Update()
    {
        if (hit)
        {
            currentTime += Time.deltaTime;
            mainSprite.material = flashMaterial;
            if (currentTime > duration)
            {
                mainSprite.material = originalMaterial;
                hit = false;
            } 
        }
    }

    private void Flash()
    {
        hit = true;
        currentTime = 0f;
    }
}
