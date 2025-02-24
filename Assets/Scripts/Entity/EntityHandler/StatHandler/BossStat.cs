using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : StatHandler
{
    [Range(1f, 100f)][SerializeField] private float skillFreq = 2f;
    public float SkillFreq
    {
        get => skillFreq;
        set => skillFreq = Mathf.Clamp(value, 0f, 100f);
    }

    [Range(0f, 100f)][SerializeField] private float detectionRange = 2f;
    public float DetectionRange
    {
        get => detectionRange;
        set => detectionRange = Mathf.Clamp(value, 0f, 100f);
    }
}
