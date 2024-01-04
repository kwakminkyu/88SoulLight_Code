using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Travel
{
    public string travelName;
    public GameObject point;
    [HideInInspector]
    public Vector2 position;
}

public class FTravelUI : MonoBehaviour
{
    public static FTravelUI instance;
    private Dictionary<string, Travel> travelDict = new();
    private Travel currentTravel;

    [SerializeField] private Transform labelCreatePosition;
    [SerializeField] private GameObject travelLabel;

    public Action<bool> OnSavePoint;
    public Action<Travel> OnTravel;
    public Action OnReset;

    private void Awake()
    {
        instance = this;

        OnTravel += SetCurrentTravel;
    }

    public void SetTravel(Travel travel)
    {
        if (travelDict.ContainsKey(travel.travelName))
            return;
        travel.point.SetActive(true);
        GameObject obj = Instantiate(travelLabel, labelCreatePosition);
        travel.point.GetComponent<TravelPoint>().SetTravel(travel);
        obj.GetComponent<TravelLabel>().SetTravel(travel);
    }

    public void SetCurrentTravel(Travel travel)
    {
        currentTravel = travel;
    }

    public void GoTravel()
    {
        GameManager.instance.player.transform.position = currentTravel.position + Vector2.up;
        OnReset?.Invoke();
        FullScreenUIManager.instance.InitCloseAll();
    }

    private void OnDisable()
    {
        OnReset?.Invoke();
    }
}
