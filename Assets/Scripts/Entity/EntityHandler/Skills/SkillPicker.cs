using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPicker : MonoBehaviour
{
    public SkillList skillList;
    public List<SkillInfo> randomSkill;
    //테스트용 UI
    public Text text1;
    public Text text2;
    public Text text3;
    public Button[] buttons;
    //테스트용 UI

    private void Start()
    {
        skillList = GetComponent<SkillList>();

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
        skillList.GetRandomSkill(3);
        randomSkill = new List<SkillInfo>(skillList.randomSkillList);
        //테스트용 UI
        text1.text = randomSkill[0].Name;
        text2.text = randomSkill[1].Name;
        text3.text = randomSkill[2].Name;
        //테스트용 UI
        for (int i = 0; i < randomSkill.Count; i++) // 디버그용
        {
            Debug.Log($"스킬 뽑기 : {randomSkill[i].Name}, {randomSkill[i].Category} , {randomSkill[i].Value}");
        }
    }

    public void SelectSkill(int select)
    {
        Debug.Log($"뽑은 스킬 : {randomSkill[select].Name}, {randomSkill[select].Category} , {randomSkill[select].Value} ");
        //뽑은 스킬들 플레이어 스텟에 적용 시키기
    }
}
