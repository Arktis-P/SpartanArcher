using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private Camera camera;
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }

    private void Update()
    {
        OnLook();
    }

    public override void Death()
    {
        base.Death();
        //gameManager.GameOver();   ���� ���� �Լ�
    }

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    void OnLook()
    {
        // �̵� ������ �ٶ󺸵��� ����
        lookDirection = movementDirection;
    }

}
