using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Weakpoint")]
public class RestoreWeakpointBuff : Buff
{
    public override void Do(Action action = null)
    {
        owner.RestoreWeakpoint();
        Deactivate();
    }
}
