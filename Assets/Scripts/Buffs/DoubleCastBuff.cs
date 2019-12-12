using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/EveryXSkillBuff/DoubleCastBuff")]
public class DoubleCastBuff : EveryXSkillBuff
{
    public float doubleCastDelay;

    public override void Do(Action action)
    {
        Utility.Instance.StartCoroutine(DelayedDo());
    }

    IEnumerator DelayedDo()
    {
        var doubleCastAction = lastCastAction;
        owner.particle.PlayCastParticle();
        yield return new WaitForSeconds(doubleCastDelay);
        doubleCastAction.Do();
        owner.particle.PauseCastParticle();
    }
}
