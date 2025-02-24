using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    //<�÷��̾�>

    [Header("Player State Skills")]
    [SerializeField] private float attackRate = 0.2f; // ���� �ֱ� ����
    public float AttackRate { get => attackRate; set => attackRate = value; }

    [SerializeField] private float skillAttackRate = 0.2f;      // ��ų ���� �ֱ� ����
    public float SkillAttackRate { get => skillAttackRate; set => skillAttackRate = value; }

    [SerializeField] private float maxHealthIncrease = 100;               // ü�� ����
    public float MaxHealthIncrease { get => maxHealthIncrease; set => maxHealthIncrease = value; }

    [SerializeField] private float drainRatioIncrease = 0.2f;      // ���� Ȯ�� ����
    public float DrainRatioIncrease { get => drainRatioIncrease; set => drainRatioIncrease = value; }

    [SerializeField] private float movementSpeedIncrease = 0.2f;        // �̵� �ӵ� ����
    public float MovementSpeedIncrease { get => movementSpeedIncrease; set => movementSpeedIncrease = value; }

    [SerializeField] private float knockbackResistanceIncrease = 0.2f;  // �˹� ���� ����
    public float KnockbackResistanceIncrease { get => knockbackResistanceIncrease; set => knockbackResistanceIncrease = value; }


    //<����ü>
    [Header("Projectile Skills")]
    [SerializeField] private float projectileDamageIncrease = 5f;          // ȭ�� ������ ����
    public float ProjectileDamageIncrease { get => projectileDamageIncrease; set => projectileDamageIncrease = value; }

    [SerializeField] private float projectileSpeedIncrease = 0.2f;           // ȭ�� �ӵ� ����
    public float ProjectileSpeedIncrease { get => projectileSpeedIncrease; set => projectileSpeedIncrease = value; }

    [SerializeField] private int projectileCountIncrease = 1;             // ȭ�� ���� ����
    public int ProjectileCountIncrease { get => projectileCountIncrease; set => projectileCountIncrease = value; }

    [SerializeField] private float projectileSizeIncrease = 0.2f;            // ȭ�� ũ�� ����
    public float ProjectileSizeIncrease { get => projectileSizeIncrease; set => projectileSizeIncrease = value; }

    [SerializeField] private float projectileRangeIncrease = 0.2f;           // ȭ�� ���� ����
    public float ProjectileRangeIncrease { get => projectileRangeIncrease; set => projectileRangeIncrease = value; }

    [SerializeField] private int monsterPenetrationIncrease = 1;     // ���� ���� Ƚ�� ����
    public int MonsterPenetrationIncrease { get => monsterPenetrationIncrease; set => monsterPenetrationIncrease = value; }

    [SerializeField] private int wallPenetrationIncrease = 1;         // �� ���� Ƚ�� ����
    public int WallPenetrationIncrease { get => wallPenetrationIncrease; set => wallPenetrationIncrease = value; }

    [SerializeField] private int projectileWallBounce;               // �� ƨ��� Ƚ�� ����
    public int ProjectileWallBounce { get => projectileWallBounce; set => projectileWallBounce = value; }

    [SerializeField] private float knockbackDistanceIncrease = 0.2f;    // �˹� �Ÿ� ����
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
