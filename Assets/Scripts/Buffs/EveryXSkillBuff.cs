﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EveryXSkillBuff : Buff
{
    public int x;
    public int count;
    public Buff gainBuff;
    public Action lastCastAction;
    public Action DoAction;

    public override void Init(Thing thing)
    {
        owner = thing;
        Player.Instance.OnPlayCard += UpdateCount;
    }

    public override int UINum()
    {
        return count;
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