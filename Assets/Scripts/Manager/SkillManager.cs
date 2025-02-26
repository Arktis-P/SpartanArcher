using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingleTon<SkillManager>
{
    [SerializeField] private SkillInfo[] skillInfo;
    public SkillInfo[] SkillInfoList { get { return skillInfo; } }
    [SerializeField] private SkillInfo chagedDash;
    [SerializeField] private SkillInfo chagedFiver;

    [SerializeField] private List<SkillInfo> SkillList;
    public List<SkillInfo> randomSkillList = new List<SkillInfo>();

    public void GetRandomSkill(int count)
    {
        randomSkillList.Clear();

        if (skillInfo == null || skillInfo.Length == 0)
        {
            Debug.LogWarning("��ų ����Ʈ�� ����ֽ��ϴ�!");
            return;
        }
        SkillList = new List<SkillInfo>(skillInfo);

        while (randomSkillList.Count < count && SkillList.Count > 0)
        {
            int random = Random.Range(0, SkillList.Count);
            randomSkillList.Add(SkillList[random]);
            SkillList.RemoveAt(random);
        }
    }

    public void SetSkillPicker()
    {
        SkillPicker skillpicker = this.GetComponentInChildren<SkillPicker>();
        skillpicker.SkillPickerList();
    }
    public void ChangedDash()
    {
        skillInfo[14] = chagedDash;
    }

    public void ChangedFiver()
    {
        skillInfo[15] = chagedFiver;
    }
}
