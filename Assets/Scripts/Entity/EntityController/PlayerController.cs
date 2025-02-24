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

    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>().normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (weaponHandler == null)
        {
            return;
        }

        Vector2 direction = GetAttackDirection();
        lookDirection = direction;

        if (direction != Vector2.zero)
        {
            isAttacking = true;
        }
    }

    private Vector2 GetAttackDirection()
    {
        // 몬스터 감지 (타겟이 감지 가능한 범위 내에 있을 때)
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, statHandler.ShootingRange, LayerMask.GetMask("Enemy"));

        if (enemies.Length > 0)
        {
            // 가장 가까운 몬스터 찾기
            Transform nearestEnemy = FindNearestEnemy(enemies);
            if (nearestEnemy != null)
            {
                // 몬스터 방향으로 공격
                return (nearestEnemy.position - transform.position).normalized;
            }
        }

        // 몬스터가 없으면 이동 방향으로 공격
        return movementDirection != Vector2.zero ? movementDirection : Vector2.zero;
    }

    private Transform FindNearestEnemy(Collider2D[] enemies)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }



}
