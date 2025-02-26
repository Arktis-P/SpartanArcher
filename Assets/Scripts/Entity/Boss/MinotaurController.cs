using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Entity.Boss
{
    internal class MinotaurController : BossController
    {
        [SerializeField] private Rect patternAreas;
        [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

        private Vector3 rushPoint;
        private Vector2 rushDirection;
        public LineRenderer rushLine;

        protected override void FixedUpdate()
        {
            movementDirection = DirectionToTarget();
            base.FixedUpdate();
        }
        protected override void Movement(Vector2 direction)
        {
            direction = direction * statHandler.MoveSpeed;
            _rigidbody.velocity = direction;

            animationHandler.Move(direction);
        }
        public override void Start()
        {
            base.Start();
            StartCoroutine(PatternAction());
        }
        private void SlashAttack()
        {
            float targetDistance = DistanceToTarget();
            Vector2 targetDirection = DirectionToTarget();
            if (targetDistance <= weaponHandler.AttackRange)  // check if player is in shooting range
            {
                bossAnimationHandler.Pattern01();  // animation

                // process collision
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, targetDirection, weaponHandler.AttackRange, weaponHandler.target
                    );
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<ResourceController>().ChangeHealth(20);
                }
            }
        }

        private void RushAttack()
        {
            float targetDistance = DistanceToTarget();
            Vector2 targetDirection = DirectionToTarget();

            bossAnimationHandler.Pattern02();  // stamping animation
            
            if (targetDistance <= followRange)  // find player(target)'s location
            {
                rushDirection = targetDirection; rushPoint = target.position;  // set rush position and direction
                // show rush area (some larger)
                rushLine.SetPosition(0, transform.position);
                rushLine.SetPosition(1, rushPoint);
                
                Invoke("RushToTarget", 1f);  // wait for 1 sec and rush 
            }
        }
        private void RushToTarget()
        {
            bossAnimationHandler.Pattern02();  // play animation
            StartCoroutine(RushCoroutine());  // rush
        }
        private IEnumerator RushCoroutine()
        {
            float rushSpeed = 9f;
            float rushDuration = 0.5f;
            float timer = 0f;

            while (timer < rushDuration)
            {
                // rush to the rush point
                _rigidbody.velocity = rushDirection * rushSpeed;

                // process collition
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, rushDirection, weaponHandler.AttackRange, weaponHandler.target
                    );
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    // give damage to player
                    hit.collider.GetComponent<ResourceController>().ChangeHealth(20);
                }

                timer += Time.deltaTime;
                yield return null;
            }

            _rigidbody.velocity = Vector2.zero;  // rush end
        }

        private void WheelwindAttack()
        {
            float targetDistance = DistanceToTarget();
            Vector2 targetDirection = DirectionToTarget();

            if (targetDistance <= followRange)
            {
                bossAnimationHandler.Pattern03();

                StartCoroutine(WheelwindCoroutine(targetDirection));
            }
        }
        private IEnumerator WheelwindCoroutine(Vector2 direction)
        {
            float attackDuration = .5f;
            float timer = 0f;

            while (timer < attackDuration)
            {
                // process collision
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position, direction, weaponHandler.AttackRange, weaponHandler.target
                    );
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    // give damage to player
                    hit.collider.GetComponent<ResourceController>().ChangeHealth(20);
                }

                timer += Time.deltaTime;
                yield return null;
            }
        }
        IEnumerator PatternAction()
        {
            while (true)
            {
                Debug.Log(patternNum);
                switch (patternNum)
                {
                    case 0:
                        SlashAttack();
                        bossAnimationHandler.Pattern01();
                        break;
                    case 1:
                        RushAttack();
                        bossAnimationHandler.Pattern02();
                        break;
                    case 2:
                        WheelwindAttack();
                        bossAnimationHandler.Pattern03();
                        break;
                    case 3:
                        break;

                }

                yield return new WaitForSecondsRealtime(6);

            }
        }
    }
}
