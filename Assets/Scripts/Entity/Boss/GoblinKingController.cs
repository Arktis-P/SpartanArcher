﻿using System;
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

        public void SpawnPawn() //Pattern01
        {
            if(pawnPrefabs.Count == 0)
            {
                Debug.Log("pawnPrefabs가 설정되지 않았습니다.");
                return;
            }
            float y = transform.position.y - 2;
            spawnArea = new Vector2(transform.position.x, transform.position.y-2);
           
            //pawnPrefabs배열 수만큼 Pawn생성
            for (int i = 0; i < pawnPrefabs.Count; i++)
            {
                GameObject spawnedEnemy = Instantiate(pawnPrefabs[i], new Vector3(spawnArea.x, spawnArea.y), Quaternion.identity);
                MonsterController monsterController = spawnedEnemy.GetComponent<MonsterController>();
                //monsterController.Init(this,target);
                //activeEnemies.Add(monsterController);
            }
        }
        protected override void Update()
        {
            //테스트 용
            if(Input.GetKeyDown(KeyCode.V))
            {
                SpawnPawn();
            }

            //테스트 용
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animationHandler.Pattern01();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                animationHandler.Pattern02();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                animationHandler.Pattern03();
            }
        }
        protected override void NormalAttack()
        {


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
    }
}
