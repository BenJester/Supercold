using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/EveryXSkillBuff/EveryXActivate")]
public class EveryXDoActivateSkillBuff : EveryXSkillBuff
{
    public ActivateSkill activateSkill;
    public float castDelay;

    public override void SecondInit()
    {
        Player.Instance.OnPlayCard += UpdateCount;
        activateSkill = Instantiate(activateSkill);
        activateSkill.owner = owner;
    }

    public override void Do(Action action = null)
    {
        Utility.Instance.StartCoroutine(DelayedDo());
    }

    IEnumerator DelayedDo()
    {
        owner.particle.PlayCastParticle();
        yield return new WaitForSeconds(castDelay);
        activateSkill.CreateAction().Do();
        owner.particle.PauseCastParticle();
    }
}
