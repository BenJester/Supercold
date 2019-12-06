using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/StatsSkill")]
public class StatsBuff : Buff
{
    public int strength;

    public override void Do(Action action = null)
    {
        owner.strength += strength;
    }

    public override void Init(Thing thing)
    {
        owner = thing;
    }

    public override void End()
    {
        owner.strength -= strength;
    }
}
