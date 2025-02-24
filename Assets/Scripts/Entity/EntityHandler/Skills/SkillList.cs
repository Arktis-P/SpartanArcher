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

        skills.Add(new SkillInfo("���� �ӵ�", SkillCategory.Player, skillManager.AttackRate));
        skills.Add(new SkillInfo("��ų ���� �ӵ�", SkillCategory.Player, skillManager.SkillAttackRate));
        skills.Add(new SkillInfo("ü�� ����", SkillCategory.Player, skillManager.MaxHealthIncrease));
        skills.Add(new SkillInfo("���� Ȯ�� ����", SkillCategory.Player, skillManager.DrainRatioIncrease));
        skills.Add(new SkillInfo("�̵� �ӵ� ����", SkillCategory.Player, skillManager.MovementSpeedIncrease));
        skills.Add(new SkillInfo("�˹� ���� ����", SkillCategory.Player, skillManager.KnockbackResistanceIncrease));

        skills.Add(new SkillInfo("ȭ�� ������ ����", SkillCategory.Projectile, skillManager.ProjectileDamageIncrease));
        skills.Add(new SkillInfo("ȭ�� �ӵ� ����", SkillCategory.Projectile, skillManager.ProjectileSpeedIncrease));
        skills.Add(new SkillInfo("ȭ�� ���� ����", SkillCategory.Projectile, skillManager.ProjectileCountIncrease));
        skills.Add(new SkillInfo("ȭ�� ũ�� ����", SkillCategory.Projectile, skillManager.ProjectileSizeIncrease));
        skills.Add(new SkillInfo("ȭ�� ���� ����", SkillCategory.Projectile, skillManager.ProjectileRangeIncrease));
        skills.Add(new SkillInfo("���� ���� Ƚ�� ����", SkillCategory.Projectile, skillManager.MonsterPenetrationIncrease));
        skills.Add(new SkillInfo("�� ���� Ƚ�� ����", SkillCategory.Projectile, skillManager.WallPenetrationIncrease));
        skills.Add(new SkillInfo("�� ƨ��� Ȱ��ȭ ����", SkillCategory.Projectile, skillManager.ProjectileWallBounce));
        skills.Add(new SkillInfo("�˹� �Ÿ� ����", SkillCategory.Projectile, skillManager.KnockbackDistanceIncrease));


        // �ٸ� ��ų�鵵 �����ϰ� �߰� ����

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
            Debug.LogWarning("��ų ����Ʈ�� ����ֽ��ϴ�!");
        }
        
        while (randomSkillList.Count < count)
        {
            int random = Random.Range(0, copySkillList.Count);
            randomSkillList.Add(copySkillList[random]);
            copySkillList.RemoveAt(random);
        }
    }
}
