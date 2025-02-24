using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : StatHandler
{
    [Range(1f, 100f)][SerializeField] private float skillFreq = 2f;
    public float SkillFreq
    {
        get => skillFreq;
        set => skillFreq = Mathf.Clamp(value, 0f, 100f);
    }

    [Range(1f, 100f)][SerializeField] private float drainRatio = 2f;
    public float DrainRatio
    {
        get => drainRatio;
        set => drainRatio = Mathf.Clamp(value, 0f, 100f);
    }

    [Range(1f, 100f)][SerializeField] private float knockbackResistance = 2f;
    public float KnockbackResistance
    {
        get => knockbackResistance;
        set => knockbackResistance = Mathf.Clamp(value, 0f, 100f);
    }

    [Range(1, 100)][SerializeField] private int projectileNumber = 1;
    public int ProjectileNumber
    {
        get => projectileNumber;
        set => projectileNumber = Mathf.Clamp(value, 0, 100);
    }
}


