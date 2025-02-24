using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    [SerializeField] private List<SkillInfo> skills = new List<SkillInfo>();
    [SerializeField] private List<SkillInfo> copySkillList;
    public List<SkillInfo> randomSkillList = new List<SkillInfo>();

    private void Awake()
    {
        SkillManager skillManager = SkillManager.Instance;

        skills.Add(new SkillInfo("공격 속도", SkillCategory.Player, skillManager.AttackRate));
        skills.Add(new SkillInfo("스킬 공격 속도", SkillCategory.Player, skillManager.SkillAttackRate));
        skills.Add(new SkillInfo("체력 증가", SkillCategory.Player, skillManager.MaxHealthIncrease));
        skills.Add(new SkillInfo("흡혈 확률 증가", SkillCategory.Player, skillManager.DrainRatioIncrease));
        skills.Add(new SkillInfo("이동 속도 증가", SkillCategory.Player, skillManager.MovementSpeedIncrease));
        skills.Add(new SkillInfo("넉백 저항 증가", SkillCategory.Player, skillManager.KnockbackResistanceIncrease));

        skills.Add(new SkillInfo("화살 데미지 증가", SkillCategory.Projectile, skillManager.ProjectileDamageIncrease));
        skills.Add(new SkillInfo("화살 속도 증가", SkillCategory.Projectile, skillManager.ProjectileSpeedIncrease));
        skills.Add(new SkillInfo("화살 개수 증가", SkillCategory.Projectile, skillManager.ProjectileCountIncrease));
        skills.Add(new SkillInfo("화살 크기 증가", SkillCategory.Projectile, skillManager.ProjectileSizeIncrease));
        skills.Add(new SkillInfo("화살 범위 증가", SkillCategory.Projectile, skillManager.ProjectileRangeIncrease));
        skills.Add(new SkillInfo("몬스터 관통 횟수 증가", SkillCategory.Projectile, skillManager.MonsterPenetrationIncrease));
        skills.Add(new SkillInfo("벽 관통 횟수 증가", SkillCategory.Projectile, skillManager.WallPenetrationIncrease));
        skills.Add(new SkillInfo("벽 튕기기 활성화 여부", SkillCategory.Projectile, skillManager.ProjectileWallBounce));
        skills.Add(new SkillInfo("넉백 거리 증가", SkillCategory.Projectile, skillManager.KnockbackDistanceIncrease));


        // 다른 스킬들도 유사하게 추가 가능

        foreach (SkillInfo skill in skills)
        {
            Debug.Log($"[{skill.Category}] {skill.Name}: {skill.Value}");
        }
    }

    public void GetRandomSkill(int count)
    {
        randomSkillList.Clear();
        copySkillList = new List<SkillInfo>(skills);

        if (skills == null || skills.Count == 0)
        {
            Debug.LogWarning("스킬 리스트가 비어있습니다!");
        }
        
        while (randomSkillList.Count < count)
        {
            int random = Random.Range(0, copySkillList.Count);
            randomSkillList.Add(copySkillList[random]);
            copySkillList.RemoveAt(random);
        }
    }
}
