using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Boss
{
    internal class MinotaurController : BossController
    {

        protected override void Movement(Vector2 direction)
        {
            direction = direction * statHandler.MoveSpeed;
            _rigidbody.velocity = direction;

            animationHandler.Move(direction);
        }
        private void Start()
        {
            base.Start();
            StartCoroutine(PatternAction());
        }
        private void SlashAttack()
        {

        }

        private void RushAttack()
        {

        }

        private void WheelwindAttack()
        {

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
                        animationHandler.Pattern01();
                        break;
                    case 1:
                        RushAttack();
                        animationHandler.Pattern02();
                        break;
                    case 2:
                        WheelwindAttack();
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
