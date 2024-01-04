using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Distance
{
    Default,
    CloseRange,
    MediumRange,
    LongRange
}

public class EnemyPattern : MonoBehaviour
{
    
    private List<Func<IEnumerator>> defaultPattern = new ();
    private List<Func<IEnumerator>> closeRange = new ();
    private List<Func<IEnumerator>> mediumRange = new ();
    private List<Func<IEnumerator>> longRange = new ();

    private Distance targetDistance;

    public void SetDistance(Distance distance)
    {
        targetDistance = distance;
    }

    public void AddPattern(Distance distance, Func<IEnumerator> pattern)
    {
        switch (distance)
        {
            case Distance.CloseRange:
                closeRange.Add(pattern);
                break;
            case Distance.MediumRange:
                mediumRange.Add(pattern);
                break;
            case Distance.LongRange:
                longRange.Add(pattern);
                break;
            default:
                defaultPattern.Add(pattern);
                break;
        }
    }

    public Func<IEnumerator> GetPattern()
    {
        List<Func<IEnumerator>> list = GetPatternList(targetDistance);
        if (list.Count == 0)
            return null;
        int random;
        for (int i = 0; i < list.Count; i++)
        {
            random = Random.Range(i, list.Count);
            Func<IEnumerator> ranPattern = list[random];
            list[random] = list[i];
            list[i] = ranPattern;
        }
        random = Random.Range(0, list.Count);
        return list[random];
    }

    private List<Func<IEnumerator>> GetPatternList(Distance distance)
    {
        switch (distance)
        {
            case Distance.CloseRange:
                return closeRange;
            case Distance.MediumRange:
                return mediumRange;
            case Distance.LongRange:
                return longRange;
            default:
                return defaultPattern;
        }
    }
}   

