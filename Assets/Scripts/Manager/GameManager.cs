using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingleTon<GameManager>
{
    private ResourceController _playerResourceController;
    private UIManager uiManager;
    public PlayerController player { get; private set; }

    private MonsterManager monsterManager;
    public MonsterManager MonsterManager { get => monsterManager; }
    private MapManager mapManager;
    public MapManager MapManager { get => mapManager; }
    private SkillManager skillManager;
    private TutorialController tutorialController;

    private int stage;
    public int Stage { get => stage; }
    private int score;
    public int Score { get => score; }
    private bool onStage;
    public bool OnStage { get => onStage; }

    public bool isGod = false;

    private void Awake()
    {
        base.Awake();

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        _playerResourceController = player.GetComponent<ResourceController>();

        uiManager = FindObjectOfType<UIManager>();
        uiManager.Init(this);

        monsterManager = FindObjectOfType<MonsterManager>();

        mapManager = FindObjectOfType<MapManager>();
        mapManager.LoadRandomMap();

        skillManager = FindObjectOfType<SkillManager>();
        // skillManager.Init();

        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }

    public void StartGame()
    {
        stage = 1;  // set (or reset) stage to 1
        score = 0;  // set (or reset) score to 0
        onStage = true;  // set (or reset) boolean to false 

        uiManager.SwitchStartTitle();  // switch off start title
        uiManager.SwitchOnStageUI();  // switch on on-stage ui
        UpdateScore(score);
        mapManager.LoadRandomMap();  // load new map
        monsterManager.Init(this);  // call monster manager
    }
    // after fail, restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        StartGame();
    }
    // after clear, after skill selected, continue game
    public void ContinueGame()
    {
        if (isGod) { stage = 10; }
        else { stage++; }  // add stage
        onStage = true;

        uiManager.SwitchStageClear();  // switch off stage clear UI
        uiManager.SwitchOnStageUI();  // switch on on-stage UI again

        mapManager.LoadRandomMap();  // load new map
        monsterManager.Init(this);  // load new mosnters
    }

    // when cleared stage == all monsters dead
    public void StageClear()
    {
        onStage = false;

        uiManager.SwitchOnStageUI();  // switch off on-stage UI
        uiManager.SwitchStageClear();  // switch on stage-clear UI

        skillManager.SetSkillPicker();  // provide player options
    }
    // when failed stage == player dead
    public void StageFail()
    {
        onStage = false;

        uiManager.SwitchOnStageUI();  // switch off on-stage UI
        uiManager.SwitchStageFail();  // switch on stage-fail UI
    }

    // when score changed update score
    public void UpdateScore(int score)
    {
        this.score += score;
        uiManager.UpdateScore();
    }

    public void ToTutorial()
    {
        uiManager.SwitchStartTitle();
        uiManager.SwitchTutorial();

        tutorialController = FindObjectOfType<TutorialController>();
        tutorialController.Init();

        onStage = true;
    }

    public void Settings()
    {
        uiManager.SwitchStartTitle();
        uiManager.SwitchSettings();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
