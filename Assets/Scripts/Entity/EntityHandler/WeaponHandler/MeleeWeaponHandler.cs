using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    //충돌할 거리
    public Vector2 collideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }
    public override void Attack()
    {
        base.Attack();
        //충돌체크 RayCast
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
            collideBoxSize, 0, Vector2.zero, 0, target);
        //null이 아니라면 충돌함
        if (hit.collider != null)
        {
            //충돌체에 RC를 가져와 널 체크
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                //데미지를 준다.
                resourceController.ChangeHealth(-Power);
                //넉백 적용
                if (IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }

    //근접무기는 Rotate가 바뀌어야함.
    public override void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
