using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : StatHandler
{
    [Range(0f, 100f)][SerializeField] private float detectionRange = 5f;
    public float DetectionRange
    {
        get => detectionRange;
        set => detectionRange = Mathf.Clamp(value, 0f, 100f);
    }
}
