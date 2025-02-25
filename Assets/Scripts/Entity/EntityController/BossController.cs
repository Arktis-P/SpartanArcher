using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossController : MonoBehaviour
{
    protected StatHandler statHandler;
    protected ResourceController resourceController;
    [SerializeField] protected SpriteRenderer chracterRenderer;
    protected Rigidbody2D _rigidbody;
    private MonsterManager monsterManager;
    protected EntityAnimationHandler animationHandler;

    private Transform target;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    [SerializeField] private float followRange = 15f;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    //MonsterManager에서 호출해줘야함.
    public void Init(MonsterManager monsterManager, Transform target)
    {
        this.monsterManager = monsterManager;
        this.target = target;
    }
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<EntityAnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        resourceController = GetComponent<ResourceController>();

    }
    protected virtual void Update()
    {
        NormalAttack();
        Rotate(lookDirection);
        HandleAttackDelay();
    }
    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void Rotate(Vector2 direction)
    {
        float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotz) > 90f;

        chracterRenderer.flipX = isLeft;
    }
    protected virtual void Movement(Vector2 direction)
    {
        direction = direction * statHandler.MoveSpeed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }
        _rigidbody.velocity = direction;
        //animationHandler.Move(direction);
    }
    public void ApplyKnockback(Transform other, float power, float duration) 
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack <= statHandler.AttackFreq)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if (isAttacking && timeSinceLastAttack > statHandler.AttackFreq)
        {
            timeSinceLastAttack = 0;
            //공격
        }
    }
    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 3f);
    }
    protected virtual void NormalAttack()
    {
        if (/*weaponHandler == null || */target == null)
        {
            //타깃 없을때 제로백터가 아니라면 제로백터로 
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        //타깃과의 방향 거리 구해서 저장
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        //따라갈 거리 안에 들어왔는지
        if (distance <= followRange)
        {
            //바라보도록 방향을 타겟의 방향을 저장해준다.
            lookDirection = direction;

            //공격범위 안에 들어왔는지 
            //if (distance <= weaponHandler.AttackRange)
            //{
            //    int layerMaskTarget = weaponHandler.target;
            //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
            //        (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

            //    if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
            //    {
            //        isAttacking = true;
            //    }

            //    movementDirection = Vector2.zero;
            //    return;
            //}
            //공격범위에 없어서 이동만
            movementDirection = direction;
        }
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


}
