using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    private Transform target;
    private MonsterManager monsterManager;

    public void Init(MonsterManager monsterManager, Transform target)
    {
        this.monsterManager = monsterManager;
        this.target = target;
    }

    public void Death()
    {
        base.Death();
        monsterManager.RemoveBossOnDeath(this);
    }
}
