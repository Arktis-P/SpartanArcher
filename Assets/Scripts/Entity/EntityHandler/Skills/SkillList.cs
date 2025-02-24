using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    [SerializeField]private List<SkillInfo> skills = new List<SkillInfo>();

    void Start()
    {
        SkillManager skillManager = SkillManager.Instance;

        skills.Add(new SkillInfo("하급 공격 속도", SkillCategory.Player, skillManager.AttackRate = 1f));
        skills.Add(new SkillInfo("중급 공격 속도", SkillCategory.Player, skillManager.AttackRate = 1.5f));
        skills.Add(new SkillInfo("상급 공격 속도", SkillCategory.Player, skillManager.AttackRate = 2f));
        skills.Add(new SkillInfo("하급 스킬 공격 속도", SkillCategory.Player, skillManager.SkillAttackRate = 1f));
        skills.Add(new SkillInfo("중급 스킬 공격 속도", SkillCategory.Player, skillManager.SkillAttackRate = 1.5f));
        skills.Add(new SkillInfo("상급 스킬 공격 속도", SkillCategory.Player, skillManager.SkillAttackRate = 2f));
        skills.Add(new SkillInfo("하급 체력 증가", SkillCategory.Player, skillManager.MaxHealthIncrease = 1f));
        skills.Add(new SkillInfo("중급 체력 증가", SkillCategory.Player, skillManager.MaxHealthIncrease = 1.5f));
        skills.Add(new SkillInfo("상급 체력 증가", SkillCategory.Player, skillManager.MaxHealthIncrease = 2f));


        // 다른 스킬들도 유사하게 추가 가능

        foreach (SkillInfo skill in skills)
        {
            Debug.Log($"[{skill.Category}] {skill.Name}: {skill.Value}");
        }
    }
}
