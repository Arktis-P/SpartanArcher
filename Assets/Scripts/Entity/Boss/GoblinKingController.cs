using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Entity.Boss
{
    internal class GoblinKingController : BossController
    {
        [SerializeField] private List<GameObject> pawnPrefabs;
        private Vector2 spawnArea;

        public override void Start()
        {
            base.Start();
            StartCoroutine(PatternAction());
        }

        public void SpawnPawn() //Pattern01
        {
            if(pawnPrefabs.Count == 0)
            {
                Debug.Log("pawnPrefabs가 설정되지 않았습니다.");
                return;
            }
            float y = transform.position.y - 2;
            spawnArea = new Vector2(transform.position.x, transform.position.y-2);

            MonsterManager monsterManager = FindObjectOfType<MonsterManager>();

            //pawnPrefabs배열 수만큼 Pawn생성
            for (int i = 0; i < pawnPrefabs.Count; i++)
            {
                GameObject spawnedEnemy = Instantiate(pawnPrefabs[i], new Vector3(spawnArea.x, spawnArea.y), Quaternion.identity);
                MonsterController monsterController = spawnedEnemy.GetComponent<MonsterController>();
                
                monsterController.Init(monsterManager, testTarget);
                monsterManager.activeMonsters.Add(monsterController);
            }
        }

        protected override void HandleAction()
        {
            if (weaponHandler == null || /*target == null*/testTarget == null)
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
                if (distance <= weaponHandler.AttackRange)
                {
                    int layerMaskTarget = weaponHandler.target;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                        (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                    if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                    {
                        isAttacking = true;
                    }

                    movementDirection = Vector2.zero;
                    return;
                }
            }
        }

        private void ThrowAttack() //Pattern02
        {
            //Projectile 생기면 구현 예정


        }

        private void Eat() // Pattern03
        {
            resourceController.ChangeHealth(10);
        }

        private void JumpAndSlamAttack()
        {
            // 시간남으면
        }

        IEnumerator PatternAction()
        {
            while(true)
            {
                switch (patternNum)
                {
                    case 0:
                        SpawnPawn();
                        bossAnimationHandler.Pattern01();
                        break;
                    case 1:
                        ThrowAttack();
                        bossAnimationHandler.Pattern02();
                        break;
                    case 2:
                        Eat();
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
