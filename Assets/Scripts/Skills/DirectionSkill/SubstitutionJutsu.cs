using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/Substitution")]
public class SubstitutionJutsu : DirectionSkill
{
    public OnHitBuff onHitBuff;
    public override void Do()
    {
        onHitBuff.targetPos = targetPos;
        owner.AddBuff(gainBuff);
        owner.AddBuff(onHitBuff);
        
    }
}
