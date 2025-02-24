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
[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class SkillInfo : ScriptableObject
{
    [Header("Skill Info")]
    [SerializeField] private string name;
    public string Name { get { return name; } }

    [SerializeField] private SkillCategory category;
    public SkillCategory Category { get { return category; } }

    [Header("Player State")]
    [SerializeField] private float health;
    public float Health { get { return health; } }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] private float attackFreq;
    public float AttackFreq { get { return attackFreq; } }

    [SerializeField] private float skillFreq;
    public float SkillFreq { get { return skillFreq; } }

    [SerializeField] private float drainRatio;
    public float DrainRaio { get { return drainRatio; } }

    [SerializeField] protected float knockbackResistance;
    public float KnockbackResistance { get { return knockbackResistance; } }

    [SerializeField] private int projectileNumber;
    public int ProjectileNumber { get { return projectileNumber; } }

    [SerializeField] private float shootingRange;
    public float ShootingRange { get { return shootingRange; } }

    [Header("Projectile State")]

    [SerializeField] private float damage;
    public float Damage { get { return damage; } }

    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get { return projectileSpeed; } }

    [SerializeField] private float size;
    public float Size { get { return size; } }

    [SerializeField] private int penetration;
    public int Penetration { get {  return penetration; } }

    [SerializeField] private int reflection;
    public int Reflection { get { return reflection; } }

    [SerializeField] private float knockbackDistance;
    public float KnockbackDistance { get { return knockbackDistance; } }
    

    

}
