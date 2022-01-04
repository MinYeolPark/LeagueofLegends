using System.Collections;
using System.Collections.Generic;

public interface IAttackable
{
    IEnumerator StartAttack();
    IEnumerator StopAttack();
}