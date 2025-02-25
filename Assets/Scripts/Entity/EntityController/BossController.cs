using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    protected StatHandler statHandler;
    protected ResourceController resourceController;
    [SerializeField] private SpriteRenderer chracterRenderer;
    protected Rigidbody2D _rigidbody;
    //protected AnimationHandler animationHandler;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        resourceController = GetComponent<ResourceController>();

    }
    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
    }
    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
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
    }
    private void Movement(Vector2 direction)
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
    public virtual void Attack()
    {
        Debug.Log("공격");
    }
    protected virtual void HandleAction()
    {

    }

}
