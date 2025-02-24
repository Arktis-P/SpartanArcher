using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    [SerializeField] private float followRange = 15f;
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

    //병합 시 Override 추가 
    protected void HandleAction()
    {
        base.HandleAction();

        if(target == null || weaponHandler == null)
        {
            //적이 없다면 제로백터
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }
        
        isAttacking = false;
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        //Target의 거리가 추적거리 안쪽인지
        if(distance <= followRange)
        {
            lookDirection = direction;

            if (/*distance <= weaponHandler.AttackRange*/true)
            {
                //공격
                //이동은 X
                //int layerMaskTarget = weaponHandler.target;
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                //    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                ////레벨과 충돌했을때는 공격하지않음
                //if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                //{
                //    isAttacking = true;
                //}

                movementDirection = direction;
            }

            //공격범위가 아니면 이동
            movementDirection = direction;
        }
    }

    //병합시 override추가
    public void Death()
    {
        base.Death();
        //enemyManager.RemoveEnemyOnDeath(this);
    }

}
