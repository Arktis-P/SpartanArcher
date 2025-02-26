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

    [SerializeField] private bool desh = false;
    public bool Desh
    {
        get => desh;
        set => desh = value;
    }

    [Range(1, 5)][SerializeField] private float deshDistance = 1.5f;
    public float DeshDistance
    {
        get => deshDistance;
        set => deshDistance = Mathf.Clamp(value, 0f, 5f);
    }

    [SerializeField] private bool isFiverTime = false;
    public bool IsFiverTime
    {
        get => isFiverTime;
        set => isFiverTime = value;
    }

    [Range(1, 100)][SerializeField] private float fiverTime = 5;
    public float FiverTime
    {
        get => fiverTime;
        set => fiverTime = Mathf.Clamp(value, 0f, 100f);
    }
}


