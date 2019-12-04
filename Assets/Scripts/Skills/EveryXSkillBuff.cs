using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EveryXSkillBuff : Buff
{
    public int x;
    int count = 0;
    public Buff gainBuff;
    public Action lastCastAction;
    public Action DoAction;

    public override void Init()
    {
        Player.Instance.OnPlayCard += UpdateCount;
    }

    void UpdateCount(Action action)
    {
        lastCastAction = action;
        count += 1;
        if (count == x)
        {
            count = 0;
            if (gainBuff)
                owner.AddBuff(gainBuff);
            if (DoAction != null)
                DoAction.Do();
            Do();
        }
    }
}
