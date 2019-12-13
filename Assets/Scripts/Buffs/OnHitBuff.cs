using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/OnGetHit")]
public class OnHitBuff : Buff
{
    public Vector2 targetPos;
    public bool oneTime;
    Thing attacker;
    public override void SecondInit()
    {
        owner.OnGetHit += GotHit;
    }

    void GotHit(Thing attacker)
    {
        this.attacker = attacker;
        Trigger();
    }

    void Trigger()
    {
        if (!active) return;
        if (gainDirectionSkill != null)
        {
            gainDirectionSkill.targetPos = targetPos;
            gainDirectionSkill.owner = owner;
            gainDirectionSkill.Do();
        }
        if (oneTime) active = false;
    }

    public override void Do(Action action = null)
    {
        
            
        
    }
}
