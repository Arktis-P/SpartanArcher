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
    [SerializeField] private string skillname;
    public string Name { get { return skillname; } }

    [SerializeField] private SkillCategory category;
    public SkillCategory Category { get { return category; } }

    [Header("Player State")]
    [SerializeField] private int health;
    public int Health { get { return health; } }

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



    //<액티브>
    //피버타임 (일정 시간동안 화살 발사 속도 x배 증가)
    //대시


    //<패시브 스킬>
    //마늘 - 캐릭터 일정 범위 내 근접 시 데미지
    //책 - 캐릭터 돌면서 충돌 시 데미지
    //비둘기 - 추가 투사체 발사
    //오망성 - 맵 전체 몬스터에 일정 데미지
}
