using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    private Transform target;
    private MonsterManager monsterManager;



    public void Init(MonsterManager monsterManager, Transform target)
    {
        this.monsterManager = monsterManager;
        this.target = target;
    }
    protected float DistanceToTarget()
    {
        //position간 거리 리턴
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        //포지션을 빼서 normalized로 방향 리턴
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();
        if (target == null || weaponHandler == null)
        {
            //적이 없다면 제로백터
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }
            isStop = false;
            isAttacking = false;
            float distance = DistanceToTarget();
            Vector2 direction = DirectionToTarget();
            RaycastHit2D head = Physics2D.Raycast(transform.position, direction, 2f, (1 << LayerMask.NameToLayer("Obstacle")));
            Debug.DrawRay(transform.position, direction * monsterStat.DetectionRange, Color.green);
        if (head.collider == null)
        {
            //Target의 거리가 추적거리 안쪽인지
            if (distance <= monsterStat.DetectionRange)
            {
                lookDirection = direction;
                if (distance <= monsterStat.ShootingRange)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, monsterStat.ShootingRange,
                        (1 << LayerMask.NameToLayer("Player")));
                    if (hit.collider != null)
                    {
                        isStop = true;
                        isAttacking = true;
                        attackDirection = lookDirection;
                    }
                    movementDirection = Vector2.zero;
                    return;
                }
                //공격범위가 아니면 이동
                movementDirection = direction;
            }
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }

    //병합시 override추가
    public override void Death()
    {
        base.Death();
        animationHandler.Die();
        monsterManager.RemoveMonsterOnDeath(this);
        
    }

}
