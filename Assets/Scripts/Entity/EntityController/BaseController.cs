using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected StatHandler statHandler;
    [SerializeField] private SpriteRenderer chracterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Rigidbody2D _rigidbody;
    protected EntityAnimationHandler animationHandler;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    [SerializeField] public WeaponHandler weaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    protected bool isStop;

    private float timeSinceLastAttack = float.MaxValue;

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    public Vector2 attackDirection = Vector2.zero;

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    public bool showDebug = false;
    protected bool isPattern = false;
    protected bool isStopAll = false;

    protected MonsterStat monsterStat;
    protected ResourceController resourceController;

    protected virtual void Awake()
    {
        resourceController = GetComponent<ResourceController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<EntityAnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        monsterStat =GetComponent<MonsterStat>();

        if (weaponPrefab != null)
        {
            weaponHandler = Instantiate(weaponPrefab, weaponPivot);
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>();
        }
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        if(isPattern==false)
        {
            Movement(movementDirection);
        }
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.deltaTime;
        }
    }


    private void Rotate(Vector2 direction)
    {
        float rotz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotz) > 90f;

        chracterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotz);
        }

        weaponHandler?.Rotate(isLeft);  // 공격 방향 전환
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
        animationHandler.Move(direction);
    }

    public void ApplyKnockback(Transform other, float power, float duration)  // WeaponHandler 또는 ProjectileController에서 적용
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }


    // 플레이어 또는 몬스터 공격 속도
    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;
        if (isPattern)
        {
            return;
        }

        if (timeSinceLastAttack <= statHandler.AttackFreq)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (!isStop) timeSinceLastAttack = 0;

        if (isAttacking && timeSinceLastAttack > statHandler.AttackFreq)
        {
            timeSinceLastAttack = 0;
            Attack();   
        }
        //if (showDebug)
        //    Debug.Log("End");
    }

    
    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
        {
            weaponHandler?.Attack();
        }
    }
    

    public virtual void Death()
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

        Destroy(gameObject, 2f);
    }

    protected virtual void HandleAction()
    {

    }

    public virtual void PatternEnd()
    {
        isPattern = false;
        isStopAll = false;
    }
    public virtual void PatternStart()
    {
        isPattern = true;
    }

}
