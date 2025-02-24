using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    //<�÷��̾�>

    [Header("Player State Skills")]
    [SerializeField] private float attackRate; // ���� �ֱ� ����
    public float AttackRate { get => attackRate; set => attackRate = value; }

    [SerializeField] private float skillAttackRate;      // ��ų ���� �ֱ� ����
    public float SkillAttackRate { get => skillAttackRate; set => skillAttackRate = value; }

    [SerializeField] private float maxHealthIncrease;               // ü�� ����
    public float MaxHealthIncrease { get => maxHealthIncrease; set => maxHealthIncrease = value; }

    [SerializeField] private float drainRatioIncrease;      // ���� Ȯ�� ����
    public float DrainRatioIncrease { get => drainRatioIncrease; set => drainRatioIncrease = value; }

    [SerializeField] private float movementSpeedIncrease;        // �̵� �ӵ� ����
    public float MovementSpeedIncrease { get => movementSpeedIncrease; set => movementSpeedIncrease = value; }

    [SerializeField] private float knockbackResistanceIncrease;  // �˹� ���� ����
    public float KnockbackResistanceIncrease { get => knockbackResistanceIncrease; set => knockbackResistanceIncrease = value; }


    //<����ü>
    [Header("Projectile Skills")]
    [SerializeField] private float projectileDamageIncrease;          // ȭ�� ������ ����
    public float ProjectileDamageIncrease { get => projectileDamageIncrease; set => projectileDamageIncrease = value; }

    [SerializeField] private float projectileSpeedIncrease;           // ȭ�� �ӵ� ����
    public float ProjectileSpeedIncrease { get => projectileSpeedIncrease; set => projectileSpeedIncrease = value; }

    [SerializeField] private int projectileCountIncrease;             // ȭ�� ���� ����
    public int ProjectileCountIncrease { get => projectileCountIncrease; set => projectileCountIncrease = value; }

    [SerializeField] private float projectileSizeIncrease;            // ȭ�� ũ�� ����
    public float ProjectileSizeIncrease { get => projectileSizeIncrease; set => projectileSizeIncrease = value; }

    [SerializeField] private float projectileRangeIncrease;           // ȭ�� ���� ����
    public float ProjectileRangeIncrease { get => projectileRangeIncrease; set => projectileRangeIncrease = value; }

    [SerializeField] private int monsterPenetrationIncrease;     // ���� ���� Ƚ�� ����
    public int MonsterPenetrationIncrease { get => monsterPenetrationIncrease; set => monsterPenetrationIncrease = value; }

    [SerializeField] private int wallPenetrationIncrease;        // �� ���� Ƚ�� ����
    public int WallPenetrationIncrease { get => wallPenetrationIncrease; set => wallPenetrationIncrease = value; }

    [SerializeField] private bool projectileWallBounce;               // �� ƨ��� Ȱ��ȭ ����
    public bool ProjectileWallBounce { get => projectileWallBounce; set => projectileWallBounce = value; }

    [SerializeField] private float knockbackDistanceIncrease;    // �˹� �Ÿ� ����
    public float KnockbackDistanceIncrease { get => knockbackDistanceIncrease; set => knockbackDistanceIncrease = value; }


    //<��Ƽ��>
    //�ǹ�Ÿ�� (���� �ð����� ȭ�� �߻� �ӵ� x�� ����)
    //���



    //<�нú� ��ų>
    //���� - ĳ���� ���� ���� �� ���� �� ������
    //å - ĳ���� ���鼭 �浹 �� ������
    //��ѱ� - �߰� ����ü �߻�
    //������ - �� ��ü ���Ϳ� ���� ������
}
