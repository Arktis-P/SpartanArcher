using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPicker : MonoBehaviour
{
    public SkillList skillList;


    private void Start()
    {
        skillList = GetComponent<SkillList>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            skillList.GetRandomSkill(3);
            List<SkillInfo> randomSkill = new List<SkillInfo>(skillList.randomSkillList);
            
            
            for (int i = 0; i < randomSkill.Count; i++) // 디버그용
            {
                Debug.Log($"스킬 뽑기 : {randomSkill[i].Name}, {randomSkill[i].Category} , {randomSkill[i].Value}");
            }

        }
    }
}
