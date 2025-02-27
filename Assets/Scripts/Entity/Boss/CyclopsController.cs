using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
//using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entity.Boss
{
    internal class CyclopsController : BossController
    {
        //레이저 시작점 위치
        private Vector3 laserArea;
        //이동 방향
        private Vector2 direction;

        CyclopsLaser cyclopsLaser;

        bool isLaserAttack = false;

        public override void Start()
        {
            base.Start();
            float randomX, randomY;
            randomX = UnityEngine.Random.Range(-1.0f, 1.0f);
            randomY = UnityEngine.Random.Range(-1.0f, 1.0f);

            Vector2 vector2 = new Vector2(randomX, randomY);
            vector2 = vector2.normalized;
            //MovementDirection To Random Initialize
            movementDirection = vector2;
            cyclopsLaser = GetComponentInChildren<CyclopsLaser>();

            StartCoroutine(PatternAction());
        }

        protected override void Movement(Vector2 direction)
        {
            direction = direction * statHandler.MoveSpeed;
            _rigidbody.velocity = direction;

            animationHandler.Move(direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Wall & Obstacle Collison Enter Check
            if (collision.collider.gameObject.layer.Equals(6) || collision.collider.gameObject.layer.Equals(9))
            {
                movementDirection = Vector3.Reflect(movementDirection, collision.GetContact(0).normal);
            }
        }

        protected override void HandleAction()
        {
            if (weaponHandler == null || target == null)
            {
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
                attackDirection = lookDirection;
                //공격범위 안에 들어왔는지
                if (distance <= weaponHandler.AttackRange)
                {
                    int layerMaskTarget = weaponHandler.target;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                        (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                    if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                    {
                        isStop = true;
                        isAttacking = true;
                    }
                    return;
                }
            }
        }

        public void ThrowAttack()
        {
            //Throw Rock
            StopOnlyMovement();
            weaponHandler.ThrowAttack();
        }

        private void LaserAttack()
        {
            //LaserArea로 이동후 공격
            isLaserAttack = true;
            //레이저 켜주기.
            //LaserOn in Animation Event
            Invoke("StopLaserAttack",5f);

        }
        public void StopLaserAttack()
        {
            cyclopsLaser.gameObject.SetActive(false);
            isLaserAttack = false;
        }

        public void StompAttack()
        {
            weaponHandler.StompAttack();
        }
        IEnumerator PatternAction()
        {
            while (true)
            {
                if (isLaserAttack == true) yield return new WaitForSecondsRealtime(3); 
                yield return new WaitForSecondsRealtime(7);
                switch (patternNum)
                {
                    case 0:
                        //Call At Animation Event
                        //StompAttack
                        PatternStart();
                        StopOnlyMovement();
                        bossAnimationHandler.Pattern01();
                        //PatternEnd At Event
                        break;
                    case 1:
                        //Call At Animation Event
                        LaserAttack();
                        PatternStart();
                        StopOnlyMovement();
                        bossAnimationHandler.Pattern02();
                        //PatternEnd At Event
                        break;
                    case 2:
                        //Call At Animation Event
                        //ThrowRock
                        PatternStart();
                        bossAnimationHandler.Pattern03();
                        //PatternEnd At Event
                        break;
                    case 3:
                        break;

                }

                //isLaserAttack = false;
            }
        }
    }
}
