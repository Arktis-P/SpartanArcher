using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Update()
    {
        OnLook();
    }

    public override void Death()
    {
        base.Death();
        //gameManager.GameOver();   게임 오버 함수
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    void OnLook()
    {
        // 이동 방향을 바라보도록 설정
        lookDirection = movementDirection;
    }

}
