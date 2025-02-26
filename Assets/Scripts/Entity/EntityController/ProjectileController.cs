using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    private bool isBounce = false;
    private int _reflection = 0; // 반사 횟수
    private int _penetration = 0; // 관통 횟수


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        //일정 거리넘어가면 삭제되는 코드 임시 주석 처리
        //if (currentDuration > rangeWeaponHandler.Duration)
        //{
        //    DestroyProjectile(transform.position, false);
        //}

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;

        // if stage is over, destroy all projectile
        if (!GameManager.Instance.OnStage) DestroyProjectile(transform.position, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_reflection > 0 && levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        { // 벽 튕기는 조건문
            Vector2 normal = ((Vector2)transform.position - collision.ClosestPoint(transform.position)).normalized;
            direction = Vector2.Reflect(direction, normal);
            _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
            _reflection--;
            // 방향 백터를 기반으로 회전 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//(radian -> degree로 변환)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);
            }
            else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
            {
                // process damage
                ResourceController resourceController = collision.GetComponent<ResourceController>();
                if (resourceController != null)
                {
                    resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                    if (rangeWeaponHandler.IsOnKnockback)
                    {
                        BaseController controller = collision.GetComponent<BaseController>();
                        if (controller != null)
                        {
                            controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                        }
                    }
                }

                if (_penetration > 0)
                {
                    _penetration--;
                }
                else
                {
                    DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
                }
            }
        }
        
    }


    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler)
    {
        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;

        _penetration = rangeWeaponHandler.Penetration;
        _reflection = rangeWeaponHandler.Reflection;


        if (this.direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
