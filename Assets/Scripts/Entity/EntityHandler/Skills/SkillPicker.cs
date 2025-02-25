using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkillPicker : MonoBehaviour
{
    private SkillManager skillManager;
    private PlayerStat playerStat;
    private GameObject player;
    private RangeWeaponHandler rangeWeaponHandler;

    //�׽�Ʈ�� UI
    public Text text1;
    public Text text2;
    public Text text3;
    public Button[] buttons;
    //�׽�Ʈ�� UI

    private void Awake()
    {
        player = GameObject.Find("Player");
        skillManager = SkillManager.Instance;
    }
    private void Start()
    {
        playerStat = player.GetComponent<PlayerStat>();
        rangeWeaponHandler = player.GetComponentInChildren<RangeWeaponHandler>();
        // ��ư �迭�� ���̰� 3�̶�� ���� (�ε��� 0, 1, 2)
        for (int i = 0; i < buttons.Length; i++)
        {
            int value = i; // ���� �ε��� ���� ����
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
        //�׽�Ʈ�� UI
        text1.text = skillManager.randomSkillList[0].Name;
        text2.text = skillManager.randomSkillList[1].Name;
        text3.text = skillManager.randomSkillList[2].Name;
        //�׽�Ʈ�� UI
        for (int i = 0; i < skillManager.randomSkillList.Count; i++) // ����׿�
        {
            Debug.Log($"��ų �̱� : {skillManager.randomSkillList[i].Name}, {skillManager.randomSkillList[i].Category}");
        }
    }

    public void SelectSkill(int select)
    {
        Debug.Log($"���� ��ų : {skillManager.randomSkillList[select].Name}, {skillManager.randomSkillList[select].Category}");
        //���� ��ų�� �÷��̾� ���ݿ� ���� ��Ű��
        SkillInfo selectedSkill = skillManager.randomSkillList[select];

        switch (selectedSkill.Category)
        {
            case SkillCategory.Player:
                playerStat.Health += selectedSkill.Health;
                playerStat.MoveSpeed += selectedSkill.MoveSpeed;
                playerStat.AttackFreq += selectedSkill.AttackFreq;
                playerStat.ShootingRange += selectedSkill.ShootingRange;
                playerStat.SkillFreq += selectedSkill.SkillFreq;
                playerStat.DrainRatio += selectedSkill.DrainRaio;
                playerStat.KnockbackResistance += selectedSkill.KnockbackResistance;
                playerStat.ProjectileNumber += selectedSkill.ProjectileNumber;
                break;
            case SkillCategory.Projectile:
                //< ����ü ���� >
                //������ damage
                //�ӵ� projectileSpeed
                //ũ�� size
                //���� Ƚ�� penetration
                //�ݻ� Ƚ�� reflection
                //�˹� �Ÿ� knockbackDistance
                //waponHandler�� ���� ��Ű��
                rangeWeaponHandler.Power += selectedSkill.Damage; //������ ����
                rangeWeaponHandler.Speed += selectedSkill.ProjectileSpeed; // ����ü �ӵ� ����
                rangeWeaponHandler.BulletSize += selectedSkill.Size; // ����ü ũ�� ����
                rangeWeaponHandler.NumberofProjectilesPerShot += selectedSkill.ProjectileNumber; //����ü ���� ����
                //rangeWeaponHandler.Delay -= �߻� �ӵ� ����
                break;
            case SkillCategory.Active:
                break;
            case SkillCategory.Passive:
                break;

        }
    }
}
