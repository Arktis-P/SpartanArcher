using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    //<플레이어>

    [Header("Player State Skills")]
    [SerializeField] private float attackRate; // 공격 주기 감소
    public float AttackRate { get => attackRate; set => attackRate = value; }

    [SerializeField] private float skillAttackRate;      // 스킬 공격 주기 감소
    public float SkillAttackRate { get => skillAttackRate; set => skillAttackRate = value; }

    [SerializeField] private float maxHealthIncrease;               // 체력 증가
    public float MaxHealthIncrease { get => maxHealthIncrease; set => maxHealthIncrease = value; }

    [SerializeField] private float drainRatioIncrease;      // 흡혈 확률 증가
    public float DrainRatioIncrease { get => drainRatioIncrease; set => drainRatioIncrease = value; }

    [SerializeField] private float movementSpeedIncrease;        // 이동 속도 증가
    public float MovementSpeedIncrease { get => movementSpeedIncrease; set => movementSpeedIncrease = value; }

    [SerializeField] private float knockbackResistanceIncrease;  // 넉백 저항 증가
    public float KnockbackResistanceIncrease { get => knockbackResistanceIncrease; set => knockbackResistanceIncrease = value; }


    //<투사체>
    [Header("Projectile Skills")]
    [SerializeField] private float projectileDamageIncrease;          // 화살 데미지 증가
    public float ProjectileDamageIncrease { get => projectileDamageIncrease; set => projectileDamageIncrease = value; }

    [SerializeField] private float projectileSpeedIncrease;           // 화살 속도 증가
    public float ProjectileSpeedIncrease { get => projectileSpeedIncrease; set => projectileSpeedIncrease = value; }

    [SerializeField] private int projectileCountIncrease;             // 화살 개수 증가
    public int ProjectileCountIncrease { get => projectileCountIncrease; set => projectileCountIncrease = value; }

    [SerializeField] private float projectileSizeIncrease;            // 화살 크기 증가
    public float ProjectileSizeIncrease { get => projectileSizeIncrease; set => projectileSizeIncrease = value; }

    [SerializeField] private float projectileRangeIncrease;           // 화살 범위 증가
    public float ProjectileRangeIncrease { get => projectileRangeIncrease; set => projectileRangeIncrease = value; }

    [SerializeField] private int monsterPenetrationIncrease;     // 몬스터 관통 횟수 증가
    public int MonsterPenetrationIncrease { get => monsterPenetrationIncrease; set => monsterPenetrationIncrease = value; }

    [SerializeField] private int wallPenetrationIncrease;        // 벽 관통 횟수 증가
    public int WallPenetrationIncrease { get => wallPenetrationIncrease; set => wallPenetrationIncrease = value; }

    [SerializeField] private bool projectileWallBounce;               // 벽 튕기기 활성화 여부
    public bool ProjectileWallBounce { get => projectileWallBounce; set => projectileWallBounce = value; }

    [SerializeField] private float knockbackDistanceIncrease;    // 넉백 거리 증가
    public float KnockbackDistanceIncrease { get => knockbackDistanceIncrease; set => knockbackDistanceIncrease = value; }


    //<액티브>
    //피버타임 (일정 시간동안 화살 발사 속도 x배 증가)
    //대시



    //<패시브 스킬>
    //마늘 - 캐릭터 일정 범위 내 근접 시 데미지
    //책 - 캐릭터 돌면서 충돌 시 데미지
    //비둘기 - 추가 투사체 발사
    //오망성 - 맵 전체 몬스터에 일정 데미지
}
