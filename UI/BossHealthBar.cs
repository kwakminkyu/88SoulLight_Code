using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private string bossName;
    private EnemyStatusHandler handler;
    private int maxHealth;

    private void Awake()
    {
        handler = GetComponent<EnemyStatusHandler>();
        handler.OnDamage += ChangeHealth;
        handler.OnDeath += UnDoHealthBar;
    }

    private void OnEnable()
    {
        maxHealth = handler.GetStat().hp;
        ChangeHealth();
        textUI.text = bossName;
    }

    private void ChangeHealth()
    {
        healthBar.value = (float)handler.currentHp / maxHealth;
    }

    private void UnDoHealthBar()
    {
        textUI.transform.parent.gameObject.SetActive(false);
    }
}
