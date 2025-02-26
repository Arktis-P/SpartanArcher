using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    [SerializeField] private ActiveSkill activeSkill;
    private void Start()
    {
        activeSkill = GetComponent<ActiveSkill>();
    }
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public override void Death()
    {
        base.Death();
        //gameManager.GameOver();   ���� ���� �Լ�
    }

    void OnMove(InputValue inputValue)  // ĳ���� ������
    {
        movementDirection = inputValue.Get<Vector2>().normalized;

        if (movementDirection != Vector2.zero)
        {
            isStop = false;
        }
        else isStop = true;
    }

    void OnDash() // �뽬 ��� �߰�
    {
        activeSkill.StartDash();
        Debug.Log("Is Dash");
    }

    
    protected override void HandleAction()  // ĳ���� �ٶ󺸴� ���� ����
    {
        base.HandleAction();
        
        Vector2 direction = movementDirection;   // �⺻������ �̵� ������ �ٶ�
                                                 
        if (movementDirection.x != 0)           // �̵� ������ �¿��� ���� �ٶ󺸴� ������ ������Ʈ
        {
            direction = movementDirection;
        }
        else
        {
            direction = lookDirection;       // y ���� -1 �Ǵ� 1�� ���� ���� �ٶ󺸴� ������ �״�� ����
        }

        Vector2 enemyDirection = GetNearestEnemyDirection();  // ���� ����� �� ���� ��������

        if (enemyDirection != Vector2.zero && isStop)
        {
            direction = enemyDirection; // ������ ���Ͱ� ������ �� �������� ��ȯ
            attackDirection = enemyDirection;
            if (isStop) isAttacking = true;
        }
        else isAttacking = false;

        // ���������� �ٶ󺸴� ���� ����
        if (direction != Vector2.zero)
        {
            lookDirection = direction;
        }
    }
    
    
    private Vector2 GetNearestEnemyDirection()   // ���� ����� ������ ������ ��ȯ�ϴ� �Լ�
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, statHandler.ShootingRange, LayerMask.GetMask("Enemy"));

        if (enemies.Length == 0)
        {
            return Vector2.zero;  // ���Ͱ� ������ �⺻�� ��ȯ
        }

        Collider2D nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return (nearestEnemy.transform.position - transform.position).normalized;
    }
}
