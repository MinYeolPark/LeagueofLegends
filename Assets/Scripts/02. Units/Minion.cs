using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : BaseUnits
{
    private void Awake()
    {
        Minion minion = GetComponent<Minion>();
        minion.OnDestroy += () => Destroy(gameObject, 2f);
    }
    protected override void Destroy()
    {
        base.Destroy();     //Die이벤트 발생 2초후에 사라짐

        //NavMesh 추적 중지 및 컴포넌트 비활성화
        agent.isStopped = true;
        agent.enabled = false;

        anim.SetTrigger("Dead");
        //minionAudioPlayer.PlayOneShot(deathSound);

        //죽었을 때, 다른 오브젝트의 방해를 방지한다 
        Collider[] colliders = GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        //MinionPool.ReturnMinion(this);
    }
}
