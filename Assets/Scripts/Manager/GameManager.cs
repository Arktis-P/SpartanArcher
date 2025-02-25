using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private ResourceController _playerResourceController;
    private UIManager uiManager;
    public PlayerController player { get; private set; }

    private MonsterManager monsterManager;
    private MapManager mapManager;

    private int stage;
    public int Stage { get => stage; }

    private void Awake()
    {
        base.Awake();

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        _playerResourceController = player.GetComponent<ResourceController>();

        uiManager = FindObjectOfType<UIManager>();
        uiManager.Init();

        monsterManager = FindObjectOfType<MonsterManager>();

        mapManager = FindObjectOfType<MapManager>();
        mapManager.LoadRandomMap();

        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }

    public void StartGame()
    {
        uiManager.SwitchStartTitle();  // switch off start title
        stage = 1;  // set (or reset) stage to 1
        monsterManager.Init(this);  // call monster manager
    }
}
