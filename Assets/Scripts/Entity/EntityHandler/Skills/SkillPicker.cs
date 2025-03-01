using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkillPicker : MonoBehaviour
{
    private SkillManager skillManager;
    private PlayerStat playerStat;
    private GameObject player;
    private RangeWeaponHandler rangeWeaponHandler;

    //테스트용 UI
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public Button[] buttons;
    //테스트용 UI

    private void Awake()
    {
        player = GameObject.Find("Player");
        skillManager = SkillManager.Instance;
    }
    private void Start()
    {
        playerStat = player.GetComponent<PlayerStat>();
        rangeWeaponHandler = player.GetComponentInChildren<RangeWeaponHandler>();
        // 버튼 배열의 길이가 3이라고 가정 (인덱스 0, 1, 2)
        for (int i = 0; i < buttons.Length; i++)
        {
            int value = i; // 현재 인덱스 값을 복사
            buttons[i].onClick.AddListener(() => SelectSkill(value));
        }
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillPickerList();
        }
        if (!GameManager.Instance.OnStage)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i))) // Alpha1, Alpha2, Alpha3로 번호키 입력 처리
                {
                    SelectSkill(i); // 선택된 번호에 맞는 스킬을 적용
                }
            }
        }
    }

    public void SkillPickerList()
    {
        Debug.Log("Input R Key");
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
                playerStat.Health += selectedSkill.Health;
                playerStat.MoveSpeed += selectedSkill.MoveSpeed;
                playerStat.ShootingRange += selectedSkill.ShootingRange;
                playerStat.SkillFreq -= selectedSkill.SkillFreq;
                playerStat.DrainRatio += selectedSkill.DrainRaio;
                playerStat.KnockbackResistance += selectedSkill.KnockbackResistance;
                playerStat.ProjectileNumber += selectedSkill.ProjectileNumber;
                playerStat.AttackFreq += selectedSkill.AttackFreq; // 발사 속도 감소
                break;
            case SkillCategory.Projectile:
                //< 투사체 스탯 >
                //데미지 damage
                //속도 projectileSpeed
                //크기 size
                //관통 횟수 penetration
                //반사 횟수 reflection
                //넉백 거리 knockbackDistance
                //waponHandler에 적용 시키기
                rangeWeaponHandler.Power += selectedSkill.Damage; //데미지 증가
                rangeWeaponHandler.Speed += selectedSkill.ProjectileSpeed; // 투사체 속도 증가
                rangeWeaponHandler.BulletSize += selectedSkill.Size; // 투사체 크기 증가
                rangeWeaponHandler.NumberofProjectilesPerShot += selectedSkill.ProjectileNumber; //투사체 개수 증가
                rangeWeaponHandler.Penetration += selectedSkill.Penetration; // 관통 횟수 증가
                rangeWeaponHandler.Reflection += selectedSkill.Reflection; //투사체 반사
                break;
            case SkillCategory.Active:
                if (selectedSkill.Name == skillManager.SkillInfoList[14].Name)
                {
                    if (!playerStat.Dash && selectedSkill.Dash) UIManager.Instance.SwitchZButton();
                    playerStat.Dash = selectedSkill.Dash;
                    playerStat.DashDistance += selectedSkill.DashDistance;
                }
                else if (selectedSkill.Name == skillManager.SkillInfoList[15].Name)
                {
                    if (!playerStat.IsFeverTime && selectedSkill.IsFeverTime) UIManager.Instance.SwitchXButton();
                    playerStat.IsFeverTime = selectedSkill.IsFeverTime;
                    playerStat.FeverTime += selectedSkill.FeverTime;
                }
                
                break;
            case SkillCategory.Passive:
                break;

        }
        if (selectedSkill.Name == skillManager.SkillInfoList[14].Name)
        {
            skillManager.ChangedDash();
        }
        if (selectedSkill.Name == skillManager.SkillInfoList[15].Name)
        {
            skillManager.ChangedFever();
        }
        GameManager.Instance.ContinueGame();
    }
}
