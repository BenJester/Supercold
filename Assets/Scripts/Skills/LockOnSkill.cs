using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LockOnSkill : Skill
{
    public GameObject target;

    public override void OnKey()
    {
        Mouse.Instance.LockOnMouse(this);
    }

    public override Action CreateAction()
    {
        return new Action(owner, this, target);
    }
}
