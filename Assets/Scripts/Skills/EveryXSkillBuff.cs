using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EveryXSkillBuff : Buff
{
    public int x;
    int count = 0;
    public Buff gainBuff;

    void Start()
    {
        Player.Instance.OnPlayCard += UpdateCount;
    }

    void UpdateCount()
    {
        count += 1;
        if (count == x)
        {
            count = 0;
            Do();
        }
    }
}
