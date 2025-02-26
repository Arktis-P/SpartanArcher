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

        public void Start()
        {
            base.Start();
            StartCoroutine(PatternAction());
            movementDirection = Vector2.zero;
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
 
        protected override void NormalAttack()
        {
            base.NormalAttack();

        }

        private void ThrowAttack() //Pattern02
        {
            //Projectile 생기면 구현 예정
            Debug.Log("ThrowAttack");
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
                        animationHandler.Pattern01();
                        break;
                    case 1:
                        ThrowAttack();
                        animationHandler.Pattern02();
                        break;
                    case 2:
                        Eat();
                        animationHandler.Pattern03();
                        break;
                    case 3:
                        break;

                }
                yield return new WaitForSecondsRealtime(6);

            }
        }
    }
}
