using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public float damageMultiplier = 1f;
    private List<IBuff> activeBuffs = new List<IBuff>();

    public static event Action<IBuff> OnBuffApplied;
    public static event Action<IBuff> OnBuffRemoved;

    public void ApplyBuff(IBuff buff)
    {
        buff.Apply(this);
        activeBuffs.Add(buff);
        OnBuffApplied?.Invoke(buff);
    }

    public void RemoveBuff(IBuff buff)
    {
        buff.Remove(this);
        activeBuffs.Remove(buff);
        OnBuffRemoved?.Invoke(buff);
    }

    public void RemoveBuffAfterDuration(IBuff buff, float duration)
    {
        StartCoroutine(RemoveBuffAfterDelay(buff, duration));
    }

    private System.Collections.IEnumerator RemoveBuffAfterDelay(IBuff buff, float delay)
    {
        yield return new WaitForSeconds(delay);
        RemoveBuff(buff);
    }
}