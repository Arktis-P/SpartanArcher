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
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        gameManager.StageFail();   // when stage failed
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

    void OnFever()
    {
        activeSkill.StartFeverTime();
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

        Vector2 enemyDirection = GetNearestEnemyDirection();  // 적 감지 이후 방향값

        if (enemyDirection != Vector2.zero && isStop)  // 주변 적을 감지하면 적 방향을 조준
        {
            direction = enemyDirection;
            attackDirection = enemyDirection;
            if (isStop) isAttacking = true;
        }
        else isAttacking = false;

        lookDirection = direction;        
    }
    
    
    private Vector2 GetNearestEnemyDirection()   // ���� ����� ������ ������ ��ȯ�ϴ� �Լ�
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, statHandler.ShootingRange, LayerMask.GetMask("Enemy"));

        Debug.DrawRay(transform.position, lookDirection * statHandler.ShootingRange, Color.blue);

        if (enemies.Length == 0)
        {
            return Vector2.zero;  // 주변에 적이 없으면 이동방향을 리턴
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
