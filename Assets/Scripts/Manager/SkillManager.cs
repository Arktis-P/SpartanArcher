using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    [SerializeField] private SkillInfo[] skillInfo;


    [SerializeField] private List<SkillInfo> SkillList;
    public List<SkillInfo> randomSkillList = new List<SkillInfo>();

    public void GetRandomSkill(int count)
    {
        randomSkillList.Clear();
        SkillList = new List<SkillInfo>(skillInfo);

        if (skillInfo == null)
        {
            Debug.LogWarning("��ų ����Ʈ�� ����ֽ��ϴ�!");
        }

        while (randomSkillList.Count < count || skillInfo.Length == 0)
        {
            int random = Random.Range(0, SkillList.Count);
            randomSkillList.Add(SkillList[random]);
            SkillList.RemoveAt(random);
        }
    }
}
