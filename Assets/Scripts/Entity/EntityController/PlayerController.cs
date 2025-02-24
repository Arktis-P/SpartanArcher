using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : BaseController
{
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public override void Death()
    {
        base.Death();
        //gameManager.GameOver();   게임 오버 함수
    }

    void OnMove(InputValue inputValue)  // 캐릭터 움직임
    {
        movementDirection = inputValue.Get<Vector2>().normalized;
    }

    protected override void HandleAction()  // 캐릭터 바라보는 방향 설정
    {
        base.HandleAction();

        /*
        if (weaponHandler == null)
        {
            Debug.Log("WeaponHandler is null, returning from HandleAction.");
            return;
        }
        */
        
        Vector2 direction = movementDirection;   // 기본적으로 이동 방향을 바라봄
                                                 
        if (movementDirection.x != 0)           // 이동 방향이 좌우일 때만 바라보는 방향을 업데이트
        {
            direction = movementDirection;
        }
        else
        {
            direction = lookDirection;       // y 값이 -1 또는 1일 때는 현재 바라보는 방향을 그대로 유지
        }

        Vector2 enemyDirection = GetNearestEnemyDirection();  // 가장 가까운 적 방향 가져오기

        if (enemyDirection != Vector2.zero)
        {
            direction = enemyDirection; // 감지된 몬스터가 있으면 그 방향으로 전환
        }

        // 최종적으로 바라보는 방향 설정
        if (direction != Vector2.zero)
        {
            lookDirection = direction;
        }

        isAttacking = true;
    }

    
    private Vector2 GetNearestEnemyDirection()   // 가장 가까운 몬스터의 방향을 반환하는 함수
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, statHandler.ShootingRange, LayerMask.GetMask("Enemy"));

        if (enemies.Length == 0)
        {
            return Vector2.zero;  // 몬스터가 없으면 기본값 반환
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
