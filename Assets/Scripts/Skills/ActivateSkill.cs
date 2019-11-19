using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 瞬发技能
[CreateAssetMenu(menuName = "Skills/AcivateSkill")]
public abstract class ActivateSkill : Skill
{
    public Passive gainPassive;

    public override void OnKey()
    {
        Cast();
    }
}
