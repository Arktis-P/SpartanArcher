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
            Debug.LogWarning("스킬 리스트가 비어있습니다!");
        }

        while (randomSkillList.Count < count || skillInfo.Length == 0)
        {
            int random = Random.Range(0, SkillList.Count);
            randomSkillList.Add(SkillList[random]);
            SkillList.RemoveAt(random);
        }
    }
}
