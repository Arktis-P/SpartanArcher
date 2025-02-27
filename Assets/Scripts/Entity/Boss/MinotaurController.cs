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
            if(!isPattern)
                base.FixedUpdate();
        }
        protected override void Update()
        {
            if(!isPattern || !isStopAll)
                base.Update();
        }

        public override void Start()
        {
            base.Start();
            StartCoroutine(PatternAction());
        }
        //애니메이션 이벤트 호출
        public void SlashAttack()
        {
            StopAll();
            float targetDistance = DistanceToTarget();
            Vector2 targetDirection = DirectionToTarget();
            if (targetDistance <= weaponHandler.AttackRange)  // check if player is in shooting range
            {
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

        public void StopAll()
        {
            isStopAll = true;
            _rigidbody.velocity = Vector2.zero;
            lookDirection = Vector2.zero;
        }
        public void StopOnlyMovement()
        {
            _rigidbody.velocity = Vector2.zero;
        }
        public void RushAttack()
        {
            float targetDistance = DistanceToTarget();
            Vector2 targetDirection = DirectionToTarget();

            if (targetDistance <= followRange)  // find player(target)'s location
            {
                rushDirection = targetDirection; rushPoint = target.position;  // set rush position and direction
                // show rush area (some larger)
                rushLine.SetPosition(0, transform.position);
                rushLine.SetPosition(1, rushPoint);
                
                //Invoke("RushToTarget", 1f);  // wait for 1 sec and rush 
            }
        }

        public void RushToTarget()
        {
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
            PatternEnd();
        }

        private void WheelwindAttack()
        {
            float targetDistance = DistanceToTarget();
            Vector2 targetDirection = DirectionToTarget();

            if (targetDistance <= followRange)
            {
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
                yield return new WaitForSecondsRealtime(5);
                Debug.Log(patternNum);
                switch (patternNum)
                {
                    case 0:
                        PatternStart();
                        bossAnimationHandler.Pattern01();
                        break;
                    case 1:
                        PatternStart();
                        StopOnlyMovement();
                        bossAnimationHandler.Pattern02();
                        break;
                    case 2:
                        WheelwindAttack();
                        bossAnimationHandler.Pattern03();
                        break;
                    case 3:
                        break;

                }

                yield return new WaitForSecondsRealtime(2);

            }
        }
    }
}
