using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

enum Pattern{
    Pattern01, Pattern02, Pattern03, Pattern04,End
};

public class BossController : BaseController
{
    protected ResourceController resourceController;
    private MonsterManager monsterManager;
    protected BossAnimationHandler bossAnimationHandler;

    protected Transform target;
    //테스트용 타겟, 테스트 끝나고 target 사용
    [SerializeField] protected Transform testTarget;
    [SerializeField] protected float followRange = 15f;
    protected int patternNum;

    GameManager gameManager;

    //MonsterManager에서 호출해줘야함.
    public void Init(MonsterManager monsterManager, Transform target)
    {
        this.monsterManager = monsterManager;
        this.target = target;
    }

    public virtual void Start()
    {
        InvokeRepeating("PatternSelect", 4f, 4f);
    }
    protected override void Awake()
    {
        base.Awake();
        bossAnimationHandler = GetComponent<BossAnimationHandler>();
        resourceController = GetComponent<ResourceController>();
    }
    protected override void Movement(Vector2 direction)
    {
        base.Movement(direction);
    }

    protected override void HandleAction()
    {

        if (weaponHandler == null || /*target == null*/testTarget == null)
        {
            //타깃 없을때 제로백터가 아니라면 제로백터로 
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;

            return;
        }

        //타깃과의 방향 거리 구해서 저장
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();
        attackDirection = lookDirection;
        isAttacking = false;
        //따라갈 거리 안에 들어왔는지
        if (distance <= followRange)
        {
            //바라보도록 방향을 타겟의 방향을 저장해준다.
            lookDirection = direction;
            //공격범위 안에 들어왔는지
            if (distance <= weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }

                movementDirection = Vector2.zero;
                return;
            }
            //공격범위에 없어서 이동만
            movementDirection = direction;
        }
    }
    protected float DistanceToTarget()
    {
        //position간 거리 리턴
        //return Vector3.Distance(transform.position, target.position);
        return Vector3.Distance(transform.position, testTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        //포지션을 빼서 normalized로 방향 리턴
        //return (target.position - transform.position).normalized;
        return (testTarget.position - transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어라면 데미지를 줘야함.
    }
    protected void PatternSelect()
    {
        //patternNum = Random.Range((int)Pattern.Pattern01, (int)Pattern.End);
        patternNum = 1;
    }
}


