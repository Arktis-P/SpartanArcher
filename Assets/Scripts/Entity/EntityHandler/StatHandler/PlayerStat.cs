using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : StatHandler
{
    [Range(0.01f, 1f)][SerializeField] private float skillFreq = 0.5f;
    public float SkillFreq
    {
        get => skillFreq;
        set => skillFreq = Mathf.Clamp(value, 0.01f, 1f);
    }

    [Range(0f, 100f)][SerializeField] private float drainRatio = 0f;
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

    [SerializeField] private bool dash = false;
    public bool Dash
    {
        get => dash;
        set => dash = value;
    }

    [Range(1, 5)][SerializeField] private float dashDistance = 1.5f;
    public float DashDistance
    {
        get => dashDistance;
        set => dashDistance = Mathf.Clamp(value, 0f, 5f);
    }

    [SerializeField] private bool isFeverTime = false;
    public bool IsFeverTime
    {
        get => isFeverTime;
        set => isFeverTime = value;
    }

    [Range(1, 100)][SerializeField] private float feverTime = 5;
    public float FeverTime
    {
        get => feverTime;
        set => feverTime = Mathf.Clamp(value, 0f, 100f);
    }
}


