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
        base.Destroy();     //Die�̺�Ʈ �߻� 2���Ŀ� �����

        //NavMesh ���� ���� �� ������Ʈ ��Ȱ��ȭ
        agent.isStopped = true;
        agent.enabled = false;

        anim.SetTrigger("Dead");
        //minionAudioPlayer.PlayOneShot(deathSound);

        //�׾��� ��, �ٸ� ������Ʈ�� ���ظ� �����Ѵ� 
        Collider[] colliders = GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        //MinionPool.ReturnMinion(this);
    }
}
