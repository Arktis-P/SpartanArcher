using System;
using System.Collections;
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

        public void Start()
        {
            base.Start();
            movementDirection = new Vector2(transform.position.x+4,transform.position.y+4);
            StartCoroutine(PatternAction());
        }

        protected override void Movement(Vector2 direction)
        {
            if(!isLaserAttack)
            {
                transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime;
            }
            animationHandler.Move(direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision != null)
            {
                movementDirection = Vector3.Reflect(movementDirection, collision.GetContact(0).normal);
            }
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
        IEnumerator PatternAction()
        {
            while (true)
            {
                switch (patternNum)
                {
                    case 0:
                        StompAttack();
                        bossAnimationHandler.Pattern01();
                        break;
                    case 1:
                        LaserAttack();
                        bossAnimationHandler.Pattern02();
                        break;
                    case 2:
                        ThrowAttack();
                        bossAnimationHandler.Pattern03();
                        break;
                    case 3:
                        break;

                }

                yield return new WaitForSecondsRealtime(6);
                isLaserAttack = false;

            }
        }
    }
}
