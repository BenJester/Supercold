using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/DrawBuff")]
public class DrawBuff : Buff
{
    public int drawNum;
    public override void Do(Action action = null)
    {
        if (owner == Player.Instance.thing)
            Player.Instance.DrawCard(drawNum);
        Deactivate();
    }
}
