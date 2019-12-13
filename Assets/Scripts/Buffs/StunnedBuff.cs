using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Stunned")]
public class StunnedBuff : Buff
{
    public override void Do(Action action = null)
    {
        owner.canCast = false;
        owner.canMove = false;
        owner.HPCanvas.ShowDebuffTimeBar(duration, this);
    }

    public override void End()
    {
        foreach (var buff in owner.buffList)
        {
            if (buff.GetType() == typeof(StunnedBuff) && buff.currDuration > 0f)
                return;
        }
        owner.HPCanvas.HideDebuffTimeBar();
        owner.canCast = true;
        owner.canMove = true;
    }
}
