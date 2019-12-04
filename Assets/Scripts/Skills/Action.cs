﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Movement, ActivateSkill, LockOnSkill
}

public class Action
{
    public Thing owner;
    public ActionType actionType;
    public Vector2 targetPos;
    public GameObject target;
    public ActivateSkill activateSkill;
    public LockOnSkill lockOnSkill;

    // movement
    public Action(Thing owner, Vector2 targetPos)
    {
        actionType = ActionType.Movement;
        this.owner = owner;
        this.targetPos = targetPos;
    }

    // activate skill
    public Action(Thing owner, ActivateSkill skill)
    {
        actionType = ActionType.ActivateSkill;
        this.owner = owner;
        activateSkill = skill;
    }

    // activate skill
    public Action(Thing owner, LockOnSkill skill, GameObject target)
    {
        actionType = ActionType.LockOnSkill;
        this.owner = owner;
        lockOnSkill = skill;
        this.target = target;
    }

    public void Do()
    {
        switch (actionType)
        {
            case ActionType.Movement:
                owner.targetPos = targetPos;
                break;
            case ActionType.ActivateSkill:
                activateSkill.Do();
                break;
            case ActionType.LockOnSkill:
                lockOnSkill.target = target;
                lockOnSkill.Do();
                break;
            default:
                break;
        }
    }
}