using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : BaseStructure
{
    private readonly int hashDie = Animator.StringToHash("Die");

    protected override void Destroy()
    {
        base.Destroy();
        anim.SetTrigger(hashDie);

        Debug.Log("Game Over");
        //GameOver
    }
}
