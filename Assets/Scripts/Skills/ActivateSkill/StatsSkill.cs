using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ActivateSkill/StatsSkill")]
public class StatsSkill : ActivateSkill
{

    public override void Do()
    {
        owner.AddBuff(Instantiate(gainBuff));
    }
}
