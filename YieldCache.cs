using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코루틴 캐싱 클래스
public static class YieldCache
{
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    
    // 박싱 방지 클래스
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }

        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    // WaitForSeconds 를 Dictionary 형식으로 저장
    private static readonly Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());
    
    // key 비교 후 return or add
    public static WaitForSeconds WaitForSeconds(float time)
    {
        UnityEngine.WaitForSeconds wfs;
        if (!timeInterval.TryGetValue(time, out wfs))
            timeInterval.Add(time, wfs = new WaitForSeconds(time));

        return wfs;
    }
}
