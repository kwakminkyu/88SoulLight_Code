using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DangerSign : MonoBehaviour
{
    [SerializeField] private GameObject dangerSign;
    [SerializeField] private AudioClip danger;

    private void Awake()
    {
        dangerSign.SetActive(false);
    }

    public void OnDangerSign()
    {
        SoundManager.instance.PlayClip(danger);
        dangerSign.SetActive(true);
    }
    
    public void OffDangerSign()
    {
        dangerSign.SetActive(false);
    }
}
