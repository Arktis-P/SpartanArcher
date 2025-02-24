using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPicker : MonoBehaviour
{
    public SkillList skillList;
    public List<SkillInfo> randomSkill;
    //�׽�Ʈ�� UI
    public Text text1;
    public Text text2;
    public Text text3;
    public Button[] buttons;
    //�׽�Ʈ�� UI

    private void Start()
    {
        skillList = GetComponent<SkillList>();

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
        skillList.GetRandomSkill(3);
        randomSkill = new List<SkillInfo>(skillList.randomSkillList);
        //�׽�Ʈ�� UI
        text1.text = randomSkill[0].Name;
        text2.text = randomSkill[1].Name;
        text3.text = randomSkill[2].Name;
        //�׽�Ʈ�� UI
        for (int i = 0; i < randomSkill.Count; i++) // ����׿�
        {
            Debug.Log($"��ų �̱� : {randomSkill[i].Name}, {randomSkill[i].Category} , {randomSkill[i].Value}");
        }
    }

    public void SelectSkill(int select)
    {
        Debug.Log($"���� ��ų : {randomSkill[select].Name}, {randomSkill[select].Category} , {randomSkill[select].Value} ");
        //���� ��ų�� �÷��̾� ���ݿ� ���� ��Ű��
    }
}
