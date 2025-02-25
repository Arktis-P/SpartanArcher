using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private ResourceController _playerResourceController;
    private UIManager uiManager;
    public PlayerController player { get; private set; }
    private void Awake()
    {
        base.Awake();

        player = FindObjectOfType<PlayerController>();
        player.Init(this);
        _playerResourceController = player.GetComponent<ResourceController>();
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }
}
