using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private List<string> tutorialStrs = new List<string>();
    private int tutorialID;

    public TextMeshProUGUI tutorialText;
    public GameObject nextButton;
    private List<Image> nextButtonImages = new List<Image>();

    public GameObject player;

    private string tempText = "";  // temporary string text for typing effect
    private float typeSpeed = 0.03f;  // typing speed

    private bool isNextOK = false;

    private Vector3 startPos;
    private Vector3 endPos;
    private float checkTime = 2f;

    GameManager gameManager;
    MapManager mapManager;
    MonsterManager monsterManager;

    private void Preparation()
    {
        this.gameManager = GameManager.Instance;
        this.mapManager = gameManager.MapManager;
        this.monsterManager = gameManager.MonsterManager;

        // set basic tutorial texts
        tutorialStrs.Add("튜토리얼에 오신 것을 환영합니다."
            + "\n이곳에서 간단한 게임 조작에 대해서 알려드립니다.");
        tutorialStrs.Add("먼저 이동에 대해서 알려드리겠습니다."
            + "\n캐릭터를 이동하려면, WASD 혹은 방향키를 입력해 주세요.");
        tutorialStrs.Add("다음은 캐릭터의 공격에 대해서 알려드리겠습니다."
            + "\n캐릭터가 정지한 상태에서 사정거리 내에 있는 가장 가까운 적을 자동으로 공격합니다."
            + "\n캐릭터를 움직여 적을 사정거리 내에 위치시켜 보세요.");
        tutorialStrs.Add("반대로 적이 캐릭터를 공격하면 캐릭터가 피해를 입을 수도 있습니다.");
        tutorialStrs.Add("매 10스테이지 마다 보스가 등장합니다."
            + "\n보스는 강력한 공격과 여러 패턴으로 무장하고 있으니 조심하길 바랍니다.");
        tutorialStrs.Add("이것으로 튜토리얼을 마칩니다."
            + "\n다음을 누르면 시작 화면으로 돌아갑니다.");

        // set basic tutorial id
        tutorialID = 0;

        mapManager.LoadTutorialMap();  // load tutorial map 
    }

    public void Init()
    {
        Preparation();
        SwitchNextButton();
        UpdateText();
    }

    private void UpdateText()
    {
        StartCoroutine(TypeText(tutorialStrs[tutorialID]));
    }
    IEnumerator TypeText(string text)
    {
        isNextOK = false;
        tempText = "";

        for (int i = 0; i < text.Length; i++)
        {
            tempText += text[i];
            tutorialText.text = tempText;
            yield return new WaitForSeconds(typeSpeed);
        }

        // check next condition and switch on next button
        isNextOK = CheckConditions();
        SwitchNextButton();
    }

    // what to do when next button pressed
    public void NextButton()
    {
        if (tutorialID >= tutorialStrs.Count - 1)
        {
            tutorialID = 0;
            SceneManager.LoadScene(0);
            return;
        }
        // if not type next text
        tutorialID++;
        SwitchNextButton();
        UpdateText();
    }

    private void SwitchNextButton()
    {
        nextButtonImages = nextButton.GetComponentsInChildren<Image>().ToList();
        Button nextButtonButton = nextButton.GetComponent<Button>();

        // switch on next button when it is available
        if (isNextOK)
        {
            foreach (Image image in nextButtonImages) { image.color = Color.white; }
            nextButtonButton.interactable = true;
        }
        // swtich off next button when it should not be available
        else
        {
            foreach (Image image in nextButtonImages) { image.color = Color.grey; }
            nextButtonButton.interactable = false;
        }
    }

    // conditions for next tutorial text
    private bool CheckConditions()
    {
        switch (tutorialID)
        {
            case 0:  // no codition
                return true;
            case 1:  // move condition
                return CheckMove();
            case 2:  // attack success
                return CheckAttack();
            case 3:  // damage success
                return CheckDamage();
            case 4:  // watch boss
                return CheckBoss();
            case 5:  // no condition
                return true;
            default: return false;
        }
    }
    private bool CheckMove()
    {
        mapManager.ResetPlayerPosition();

        startPos = player.transform.position;  // player's first position
        StartCoroutine(CheckMoveCoroutine());
        return startPos != endPos;
    }
    IEnumerator CheckMoveCoroutine()
    {
        yield return new WaitForSeconds(checkTime);
        endPos = transform.position;
    }

    private bool CheckAttack()
    {
        StartCoroutine(CheckAttackCoroutine());
        return monsterManager.IsTutorialClear;
    }
    IEnumerator CheckAttackCoroutine()
    {
        mapManager.ResetPlayerPosition();
        monsterManager.Init(gameManager);

        // wait until IsTutorialClear true
        yield return new WaitUntil(() => monsterManager.IsTutorialClear);
    }

    private bool CheckDamage()
    {
        mapManager.ResetPlayerPosition();
        monsterManager.Init(gameManager);

        return true;
    }
    private bool CheckBoss()
    {
        mapManager.ResetPlayerPosition();
        monsterManager.Init(gameManager);
        monsterManager.SpawnRandomBossPublic();

        return true;
    }
}
