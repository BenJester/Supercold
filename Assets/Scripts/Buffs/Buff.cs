using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public string buffName = "New Buff";
    public string detail = "This is a buff";
    public float duration = -1f;
    public float currDuration;
    public Thing owner;
    
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

    public void RemoveFromList()
    {
        //TODO
        //owner.buffList.Remove(this);
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
            RemoveFromList();
        }
        else
            currDuration -= Time.fixedDeltaTime;

    }
}
