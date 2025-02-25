using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>
{
    public HealthbarController playerHealthBar;
    // public HealthbarController bossHealthBar;

    private bool isOnStartTitle = false;
    private bool isOnStage = false;
    private bool isOnClear = false;
    private bool isOnFail = false;
    private int stage;

    public GameObject startTitle;
    public GameObject onStageUI;
    public Text stageText;
    public GameObject stageClearUI;
    public GameObject stageFailUI;
    public Text scoreText;

    private GameManager gameManager;

    // Start is called before the first frame update
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;

        CheckErrors();  // check exceptions

        playerHealthBar.Init();

        SwitchStartTitle();
    }

    // check exceptions
    private void CheckErrors()
    {
        if (startTitle == null) { Debug.LogError("Object Start Title is NOT connected!"); }
        if (onStageUI == null) { Debug.LogError("Object On Stage UI is NOT connected!"); }
        if (stageText == null) { Debug.LogError("Stage Text is NOT connected!"); }
        if (stageClearUI == null) { Debug.LogError("Object Stage Clear UI is NOT connected!"); }
        if (stageFailUI == null) { Debug.LogError("Object Stage Fail UI is NOT connected!"); }
        if (scoreText == null) { Debug.LogError("Score Text is NOT connected!"); }
    }

    // switch on/off start title
    public void SwitchStartTitle()
    {
        isOnStartTitle = !isOnStartTitle;
        startTitle.SetActive(isOnStartTitle);
    }
    // switch on/off on stage UIs
    public void SwitchOnStageUI()
    {
        isOnStage = !isOnStage;
        onStageUI.SetActive(isOnStage);  // switch entire on Stage UI
        stageText.text = gameManager.Stage.ToString();  // update stage text
    }
    // switch on stage clear UI
    public void SwitchStageClear()
    {
        isOnClear = !isOnClear;
        stageClearUI.SetActive(isOnClear);
    }
    // switch on stage fail UI
    public void SwitchStageFail()
    {
        isOnFail = !isOnFail;
        stageFailUI.SetActive(isOnFail);
    }

    public void UpdateScore()
    {
        scoreText.text = gameManager.Score.ToString();
    }
}
