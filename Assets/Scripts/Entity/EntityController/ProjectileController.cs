using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    public bool FragmentProjectile { get; set; }

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    private bool isBounce = false;
    private int _reflection = 0; // �ݻ� Ƚ��
    private int _penetration = 0; // ���� Ƚ��


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

        //���� �Ÿ��Ѿ�� �����Ǵ� �ڵ� �ӽ� �ּ� ó��
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
        { // �� ƨ��� ���ǹ�
            Vector2 normal = ((Vector2)transform.position - collision.ClosestPoint(transform.position)).normalized;
            direction = Vector2.Reflect(direction, normal);
            _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
            _reflection--;
            // ���� ���͸� ������� ȸ�� 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//(radian -> degree�� ��ȯ)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
            {
                if (FragmentProjectile)
                {
                    ProjectileManager.Instance.ShootBullet(rangeWeaponHandler, collision.ClosestPoint(transform.position) + CheckDir(collision) * 1.3f, Vector2.up);
                    ProjectileManager.Instance.ShootBullet(rangeWeaponHandler, collision.ClosestPoint(transform.position) + CheckDir(collision) * 1.3f, Vector2.down);
                    ProjectileManager.Instance.ShootBullet(rangeWeaponHandler, collision.ClosestPoint(transform.position) + CheckDir(collision) * 1.3f, Vector2.left);
                    ProjectileManager.Instance.ShootBullet(rangeWeaponHandler, collision.ClosestPoint(transform.position) + CheckDir(collision) * 1.3f, Vector2.right);
                }
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

    public Vector2 CheckDir(Collider2D collision)
    {
        Vector2 collisionPosition = collision.ClosestPoint(transform.position);
        Vector2 dir = ((Vector2)transform.position - collisionPosition).normalized;
        float x;
        float y;
        x = Mathf.Round(dir.x);
        y = Mathf.Round(dir.y);
        
        dir = new Vector2(x,y);

        if (dir.x == 1f)
        {
            return Vector2.right;
        }
        else if (dir.y == 1f)
        {
            return Vector2.up;
        }
        else if (dir.y == -1f)
        {
            return Vector2.down;
        }
        else if (dir.x == -1f)
        {
            return Vector2.left;
        }
        return Vector2.left;
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
