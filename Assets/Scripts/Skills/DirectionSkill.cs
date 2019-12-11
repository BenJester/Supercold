using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionSkill : Skill
{
    public Vector2 targetPos;
    public bool useReletivePos;

    public override void OnKey()
    {
        Mouse.Instance.SelectDirection(this);
    }

    public override Action CreateAction()
    {
        return new Action(owner, this, targetPos);
    }
}
