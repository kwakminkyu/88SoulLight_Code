using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSenceTest : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera cameraOnLeft;
    [SerializeField] public CinemachineVirtualCamera cameraOnRight;
    [SerializeField] public float waitTime;
    private LayerMask playerLayer;

    private void Awake()
    {
        playerLayer = 1 << LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            CameraManager.instance.CutSenceCamera(cameraOnLeft, cameraOnRight, waitTime);
        }
    }
}
