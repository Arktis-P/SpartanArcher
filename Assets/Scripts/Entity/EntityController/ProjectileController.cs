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
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestroy);
            }
            else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
            {
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
