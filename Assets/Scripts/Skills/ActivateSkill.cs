﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 瞬发技能
public abstract class ActivateSkill : Skill
{

    public override void OnKey()
    {
        Cast();
    }

    public override Action CreateAction()
    {
        return new Action(owner, this);
    }
}
