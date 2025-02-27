using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private List<string> tutorialStrs = new List<string>();
    private int tutorialID;

    public TextMeshProUGUI tutorialText;

    private string tempText = "";  // temporary string text for typing effect
    private float typeSpeed = 0.1f;  // typing speed

    // Start is called before the first frame update
    void Start()
    {
        // set basic tutorial texts
        tutorialStrs.Add("튜토리얼에 오신 것을 환영합니다."
            + "\n이곳에서 간단한 게임 조작에 대해서 알려드립니다.");
        tutorialStrs.Add("먼저 이동에 대해서 알려드리겠습니다."
            + "\n캐릭터를 이동하려면, WASD 혹은 방향키를 입력해 주세요.");
        tutorialStrs.Add("다음은 캐릭터의 공격에 대해서 알려드리겠습니다."
            + "\n캐릭터가 움직이지 정지한 상태에서 사정거리 내에 있는 가장 가까운 적을 자동으로 공격합니다."
            + "\n캐릭터를 움직여 적을 사정거리 내(빨간 원 안)에 위치시켜 보세요.");
        tutorialStrs.Add("반대로 적이 캐릭터를 공격하면 캐릭터가 피해를 입을 수도 있습니다.");
        tutorialStrs.Add("매 10스테이지 마다 보스가 등장합니다."
            + "\n보스는 강력한 공격과 여러 패턴으로 무장하고 있으니 조심하길 바랍니다.");
        tutorialStrs.Add("이것으로 튜토리얼을 마칩니다."
            + "\n다음을 누르면 시작 화면으로 돌아갑니다.");

        // set basic tutorial id
        tutorialID = 0;
    }

    private void UpdateText()
    {
        StartCoroutine(TypeText(tutorialStrs[tutorialID]));
    }
    IEnumerator TypeText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            tempText += text[i];
            tutorialText.text = tempText;
            yield return new WaitForSeconds(typeSpeed);
        }
    }
    private void NextText()
    {
        if (tutorialID >= tutorialStrs.Count) tutorialID = 0;
        else tutorialID++;
    }
}
