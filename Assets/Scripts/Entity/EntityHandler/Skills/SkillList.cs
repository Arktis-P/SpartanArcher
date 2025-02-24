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

        skills.Add(new SkillInfo("�ϱ� ���� �ӵ�", SkillCategory.Player, skillManager.AttackRate = 1f));
        skills.Add(new SkillInfo("�߱� ���� �ӵ�", SkillCategory.Player, skillManager.AttackRate = 1.5f));
        skills.Add(new SkillInfo("��� ���� �ӵ�", SkillCategory.Player, skillManager.AttackRate = 2f));
        skills.Add(new SkillInfo("�ϱ� ��ų ���� �ӵ�", SkillCategory.Player, skillManager.SkillAttackRate = 1f));
        skills.Add(new SkillInfo("�߱� ��ų ���� �ӵ�", SkillCategory.Player, skillManager.SkillAttackRate = 1.5f));
        skills.Add(new SkillInfo("��� ��ų ���� �ӵ�", SkillCategory.Player, skillManager.SkillAttackRate = 2f));
        skills.Add(new SkillInfo("�ϱ� ü�� ����", SkillCategory.Player, skillManager.MaxHealthIncrease = 1f));
        skills.Add(new SkillInfo("�߱� ü�� ����", SkillCategory.Player, skillManager.MaxHealthIncrease = 1.5f));
        skills.Add(new SkillInfo("��� ü�� ����", SkillCategory.Player, skillManager.MaxHealthIncrease = 2f));


        // �ٸ� ��ų�鵵 �����ϰ� �߰� ����

        foreach (SkillInfo skill in skills)
        {
            Debug.Log($"[{skill.Category}] {skill.Name}: {skill.Value}");
        }
    }
}
