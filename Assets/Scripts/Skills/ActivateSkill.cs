using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 瞬发技能
[CreateAssetMenu(menuName = "Skills/AcivateSkill")]
public abstract class ActivateSkill : Skill
{
    public Buff gainBuff;

    public override void OnKey()
    {
        Cast();
    }

    public override Action CreateAction()
    {
        return new Action(owner, this);
    }
}
