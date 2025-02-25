using System;
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
        protected override void Update()
        {
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
        private void SlashAttack()
        {

        }

        private void RushAttack()
        {

        }

        private void WheelwindAttack()
        {

        }
    }
}
