using Assets.Scripts.Entity.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEvent : MonoBehaviour
{
    BossController bossController;
    GoblinKingController goblinKingController;
    MinotaurController minotaurController;
    private void Start()
    {
        bossController = GetComponentInParent<BossController>();
        goblinKingController = bossController.GetComponentInChildren<GoblinKingController>();
        minotaurController = bossController.GetComponentInChildren<MinotaurController>();
    }

    public void PatternEnd()
    {
        bossController.PatternEnd();
    }

    public void PatternThrowAttack()
    {
        goblinKingController.ThrowAttack();
    }

    public void PatternSpawnPawn()
    {
        goblinKingController.SpawnPawn();
    }

    public void PatternSlash()
    {
        minotaurController.SlashAttack();
    }
    public void PatternRushAttack()
    {
        minotaurController.RushAttack();
    }
    public void PatternRushToTarget()
    {
        minotaurController.RushToTarget();
    }
}
