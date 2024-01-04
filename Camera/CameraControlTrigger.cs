using Cinemachine;
using UnityEngine;

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

public class CameraControlTrigger : MonoBehaviour
{
    public CameraManager _cameraManager;

    private Collider2D coll;
    private LayerMask playerLayer;

    [Header("Swap Camera")]
    public CinemachineVirtualCamera cameraOnLeft;
    public CinemachineVirtualCamera cameraOnRight;

    [Header("Pan Camera")]
    public PanDirection panDirection;
    public float panDistance = 3f;
    public float panTime = 0.35f;

    public bool usingPanCamera;
    public bool usingSwapCamera;


    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        playerLayer = 1 << LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        _cameraManager = CameraManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            if (usingPanCamera)
            {
                _cameraManager.PanCameraOnContact(panDistance, panTime, panDirection, false);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer)))
        {
            Vector2 exitDirection = (collision.transform.position - coll.bounds.center).normalized;
            
            if (usingSwapCamera)
            {
                _cameraManager.SwapCamera(cameraOnLeft, cameraOnRight, exitDirection);
            }
            
            if (usingPanCamera)
            {
                _cameraManager.PanCameraOnContact(panDistance,panTime, panDirection, true);
            }
        }
    }
}



