using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Movement, ActivateSkill, LockOnSkill, DirectionSkill
}

public class Action
{
    public Thing owner;
    public ActionType actionType;
    public Vector2 targetPos;
    public GameObject target;
    public ActivateSkill activateSkill;
    public LockOnSkill lockOnSkill;
    public DirectionSkill directionSkill;
    public string actionName;
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
        actionName = skill.skillName;
    }

    // lock on skill
    public Action(Thing owner, LockOnSkill skill, GameObject target)
    {
        actionType = ActionType.LockOnSkill;
        this.owner = owner;
        lockOnSkill = skill;
        this.target = target;
        actionName = skill.skillName;
    }

    // direction skill
    public Action(Thing owner, DirectionSkill skill, Vector2 pos)
    {
        actionType = ActionType.DirectionSkill;
        this.owner = owner;
        directionSkill = skill;
        if (skill.useReletivePos)
            targetPos = ((Vector3)pos - owner.transform.position).normalized;
        else
            targetPos = pos;
        actionName = skill.skillName;
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
            case ActionType.DirectionSkill:
                directionSkill.targetPos = targetPos;
                directionSkill.Do();
                break;
            default:
                break;
        }
    }

    public void CastTimeDo()
    {
        switch (actionType)
        {
            case ActionType.Movement:
                owner.targetPos = targetPos;
                break;
            case ActionType.ActivateSkill:
                Utility.Instance.StartCoroutine(activateSkill.CastTime());
                break;
            case ActionType.LockOnSkill:
                lockOnSkill.target = target;
                Utility.Instance.StartCoroutine(lockOnSkill.CastTime());
                break;
            case ActionType.DirectionSkill:
                directionSkill.targetPos = targetPos;
                Utility.Instance.StartCoroutine(directionSkill.CastTime());
                break;
            default:
                break;
        }
    }
}
