using Assets.Scripts.Entity.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEvent : MonoBehaviour
{
    BossController bossController;
    GoblinKingController goblinKingController;
    private void Start()
    {
        bossController = GetComponentInParent<BossController>();
        goblinKingController = bossController.GetComponentInChildren<GoblinKingController>();
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
}
