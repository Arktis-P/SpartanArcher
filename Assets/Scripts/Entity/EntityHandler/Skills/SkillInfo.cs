using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillCategory
{
    Player,
    Projectile,
    Active,
    Passive
}

public class SkillInfo
{
    public string Name { get; private set; }
    public SkillCategory Category { get; private set; }
    public float Value { get; private set; }

    public SkillInfo(string name, SkillCategory category, float value)
    {
        Name = name;
        Category = category;
        Value = value;
    }
}
