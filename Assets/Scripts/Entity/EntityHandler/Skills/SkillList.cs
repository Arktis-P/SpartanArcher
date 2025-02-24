using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    [SerializeField] private List<SkillInfo> skills = new List<SkillInfo>();
    [SerializeField] private List<SkillInfo> copySkillList;
    public List<SkillInfo> randomSkillList = new List<SkillInfo>();

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
