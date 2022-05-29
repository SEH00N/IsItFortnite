using System.Collections;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance = null;

    /// <summary>
    /// 타임 프리징
    /// </summary>
    public void ModifyTimeScale(float timeVal, float dura, Action Oncomplete = null)
    {
        StartCoroutine(TimeFreeze(timeVal, dura, Oncomplete));
    }

    /// <summary>
    /// 타임 프리징
    /// </summary>
    public IEnumerator TimeFreeze(float timeVal, float dura, Action Oncomplete = null)
    {
        yield return new WaitForSecondsRealtime(dura);
        Time.timeScale = timeVal;
        Oncomplete?.Invoke();
    }
}
