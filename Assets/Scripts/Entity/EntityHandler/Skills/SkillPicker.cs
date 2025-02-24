using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPicker : MonoBehaviour
{
    private SkillManager skillManager;

    private PlayerStat playerStat;
    //테스트용 UI
    public Text text1;
    public Text text2;
    public Text text3;
    public Button[] buttons;
    //테스트용 UI

    private void Awake()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        skillManager = SkillManager.Instance;
    }
    private void Start()
    {

        // 버튼 배열의 길이가 3이라고 가정 (인덱스 0, 1, 2)
        for (int i = 0; i < buttons.Length; i++)
        {
            int value = i; // 현재 인덱스 값을 복사
            buttons[i].onClick.AddListener(() => SelectSkill(value));
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SkillPickerList();
            

        }
    }

    public void SkillPickerList()
    {
        Debug.Log("Input A Key");
        skillManager.GetRandomSkill(3);
        //테스트용 UI
        text1.text = skillManager.randomSkillList[0].Name;
        text2.text = skillManager.randomSkillList[1].Name;
        text3.text = skillManager.randomSkillList[2].Name;
        //테스트용 UI
        for (int i = 0; i < skillManager.randomSkillList.Count; i++) // 디버그용
        {
            Debug.Log($"스킬 뽑기 : {skillManager.randomSkillList[i].Name}, {skillManager.randomSkillList[i].Category}");
        }
    }

    public void SelectSkill(int select)
    {
        Debug.Log($"뽑은 스킬 : {skillManager.randomSkillList[select].Name}, {skillManager.randomSkillList[select].Category}");
        //뽑은 스킬들 플레이어 스텟에 적용 시키기
        SkillInfo selectedSkill = skillManager.randomSkillList[select];

        switch (selectedSkill.Category)
        {
            case SkillCategory.Player:
                playerStat.Health += Mathf.RoundToInt(selectedSkill.Health);
                playerStat.MoveSpeed += selectedSkill.MoveSpeed;
                playerStat.AttackFreq += selectedSkill.AttackFreq;
                playerStat.ShootingRange += selectedSkill.ShootingRange;
                playerStat.SkillFreq += selectedSkill.SkillFreq;
                playerStat.DrainRatio += selectedSkill.DrainRaio;
                playerStat.KnockbackResistance += selectedSkill.KnockbackResistance;
                playerStat.ProjectileNumber += selectedSkill.ProjectileNumber;
                break;
            case SkillCategory.Projectile:

                break;
            case SkillCategory.Active:
                break;
            case SkillCategory.Passive:
                break;

        }
    }
}
