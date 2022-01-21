using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhibitator : BaseStructure
{
    private readonly int hashDie = Animator.StringToHash("Die");

    protected override void Destroy()
    {
        base.Destroy();
        anim.SetTrigger(hashDie);

        Debug.Log("Super Minion Respawn");
        //GameOver
    }
}
