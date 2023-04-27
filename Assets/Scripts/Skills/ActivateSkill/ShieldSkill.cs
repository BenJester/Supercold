using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ActivateSkill/ShieldSkill")]
public class ShieldSkill : ActivateSkill
{
    public int shieldAmount;
    public bool groupShield;

    public override void Do()
    {
        if (!groupShield)
        {
            if (owner.hp == owner.maxHp)
                owner.maxHp += shieldAmount;
            owner.hp += shieldAmount;
        }
            
        else
        {
            foreach (var actor in Player.Instance.actors)
            {
                if (actor.thing.hp == actor.thing.maxHp)
                    actor.thing.maxHp += shieldAmount;
                actor.thing.hp += shieldAmount;
            }
        }
            
    }
}
