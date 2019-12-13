using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ActivateSkill/Mimic")]
public class MimicSkill : ActivateSkill
{
    public int num;
    public override void Do()
    {
        if (owner == Player.Instance.thing)
        {
            for (int i = 0; i < num; i ++)
            {
                if (owner.lastCastAction.skill.GetType() != this.GetType())
                    Player.Instance.DrawCard(Instantiate(owner.lastCastAction.skill));
            }
            
        }
    }

}
