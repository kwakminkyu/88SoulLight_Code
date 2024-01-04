using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelPoint : MonoBehaviour
{
    private Travel travel;
    private Color activeColor;
    [SerializeField] private Image point;

    private void Awake()
    {
        activeColor = point.color;
        gameObject.SetActive(false);
    }

    private void SelectPoint(Travel travel)
    {
        if (travel.travelName == this.travel.travelName)
            point.rectTransform.sizeDelta = new Vector2(50, 50);
        else
            ResetPoint();
    }

    public void SetTravel(Travel travel)
    {
        this.travel = travel;
        FTravelUI.instance.OnTravel += SelectPoint;
        FTravelUI.instance.OnSavePoint += ReadyTravel;
        FTravelUI.instance.OnReset += ResetPoint;
    }

    public void ReadyTravel(bool savePoint)
    {
        if (savePoint)
        {
            point.color = activeColor;
        }
        else
        {
            Color inActiveColor = activeColor;
            inActiveColor.a = 0.5f;
            point.color = inActiveColor;
        }
    }

    public void ResetPoint()
    {
        point.rectTransform.sizeDelta = new Vector2(30, 30);
    }
}
