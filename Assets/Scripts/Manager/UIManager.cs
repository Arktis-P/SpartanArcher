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
    private int stage;

    public GameObject startTitle;
    public GameObject onStageUI;
    public Text stageText;

    private GameManager gameManager;

    // Start is called before the first frame update
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        playerHealthBar.Init();

        SwitchStartTitle();
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
}
