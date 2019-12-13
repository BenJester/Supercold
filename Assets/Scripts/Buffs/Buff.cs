using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    
    public bool active = true;
    public string buffName = "New Buff";
    public string detail = "This is a buff";
    public float duration = -1f;
    [HideInInspector]
    public float currDuration;
    [HideInInspector]
    public Thing owner;
    public DirectionSkill gainDirectionSkill;

    public abstract void Do(Action action = null);

    public void Init(Thing thing)
    {
        currDuration = duration;
        owner = thing;
        SecondInit();
    }

    public virtual void SecondInit()
    {
    }

    public void Deactivate()
    {
        active = false;
    }

    public virtual void End()
    {
    }
    public virtual int UINum()
    {
        return 1;
    }

    public void Tick()
    {
        if (duration == -1f || currDuration == -1f)
            return;
        
        if (currDuration <= 0)
        {
            End();
            currDuration = -1f;
            Deactivate();
        }
        else
            currDuration -= Time.fixedDeltaTime;

    }
}
