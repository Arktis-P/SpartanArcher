using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationHandler : EntityAnimationHandler
{
    public void Pattern01()
    {
        animator.SetTrigger("Pattern01");
    }
    public void Pattern02()
    {
        animator.SetTrigger("Pattern02");
    }
    public void Pattern03()
    {
        animator.SetTrigger("Pattern03");
    }
    public void Pattern04()
    {
        animator.SetTrigger("Pattern04");
    }

    public void LaserPatternEnd()
    {
        animator.SetTrigger("IsLaserEnd");
    }
}
