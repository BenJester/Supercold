using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/StatsSkill")]
public class StatsBuff : Buff
{
    public int strength;
    public float speedPercentage;
    public int shield;

    public override void Do(Action action = null)
    {
        owner.strength += strength;
        owner.speedMultiplier += speedPercentage;
        owner.shield += shield;
    }

    public override void End()
    {
        owner.strength -= strength;
        owner.speedMultiplier -= speedPercentage;
        owner.shield -= shield;
    }
}
