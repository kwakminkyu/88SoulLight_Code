using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitTravel : MonoBehaviour
{
    private bool isReady;

    private void Start()
    {
        FTravelUI.instance.OnSavePoint += ChangeReady;
    }

    public void OnSubmitTravel()
    {
        if (isReady)
            FTravelUI.instance.GoTravel();
    }

    public void ChangeReady(bool savePoint)
    {
        isReady = savePoint;
    }
}
