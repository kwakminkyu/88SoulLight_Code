using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TravelLabel : MonoBehaviour
{
    private Travel travel;
    [SerializeField] private TextMeshProUGUI textUI;

    public void SetTravel(Travel travel)
    {
        this.travel = travel;
        textUI.text = travel.travelName;
    }

    public void SelectLabel()
    {
        FTravelUI.instance.OnTravel?.Invoke(travel);
    }
}
