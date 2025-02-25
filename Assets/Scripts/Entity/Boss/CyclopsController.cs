using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Entity.Boss
{
    internal class CyclopsController : BossController
    {
        //레이저 시작점 기즈모
        [SerializeField]
        private Rect patternAreas; 
        [SerializeField]
        private Color gizmoColor = new Color(1, 0, 0, 0.3f);

        //레이저 시작점 위치
        private Vector3 laserArea;
        //이동 방향
        private Vector2 direction;

        bool isLaserAttack = false;

        private void Start()
        {
            direction = new Vector2(transform.position.x+4,transform.position.y+4);
        }

        protected override void Update()
        {
            if (!isLaserAttack)
            {
                MoveAttack();
            }
            if (isLaserAttack)
                LaserAttack();

            //테스트 용
            if (Input.GetKeyDown(KeyCode.V))
            {
                LaserAttack();
            }
        }
        public override void Attack()
        {
            

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision != null)
            {
                direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
            }
        }
        private void MoveAttack()
        {
           //왼쪽방향으로 가는지 오른쪽으로 가는지에 따라 Sprtie Flip 시켜주어야함.
           transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime;

            //Projectile 일정주기 발사
        }

        private void ThrowAttack()
        {

        }

        private void LaserAttack()
        {
            //LaserArea로 이동후 공격
            isLaserAttack = true;

            transform.position = Vector2.Lerp(transform.position,laserArea,Time.deltaTime);

        }
        private void OnDrawGizmosSelected()
        {
            if (patternAreas == null) return;

            Gizmos.color = gizmoColor;
            Vector3 center = new Vector3(patternAreas.x + patternAreas.width / 2, patternAreas.y + patternAreas.height / 2);
            Vector3 size = new Vector3(patternAreas.width, patternAreas.height);
            Gizmos.DrawCube(center, size);
            laserArea = center;
        }

        private void StompAttack()
        {

        }
    }
}
