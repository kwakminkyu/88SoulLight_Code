using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator anim;

    public readonly int run = Animator.StringToHash("Run");
    public readonly int attack = Animator.StringToHash("Attack");
    public readonly int idle = Animator.StringToHash("Idle");
    public readonly int death = Animator.StringToHash("Death");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void HashTrigger(int hash)
    {
        anim.SetTrigger(hash);
    }

    public void HashBool(int hash, bool value)
    {
        anim.SetBool(hash, value);
    }

    public void StringTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public void StringBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }
}
