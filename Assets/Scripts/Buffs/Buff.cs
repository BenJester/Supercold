using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
    public string buffName = "New Buff";
    public string detail = "This is a buff";
    public float duration = -1f;
    public Thing owner;
    
    public abstract void Do(Action action = null);
    public abstract void Init(Thing thing);
    public virtual void End()
    {
    }
    public virtual int UINum()
    {
        return -1;
    }

    public void Tick()
    {
        if (duration == -1f)
            return;
        duration -= Time.fixedDeltaTime;
        if (duration < 0)
        {
            End();
            duration = -1;
        }
            
    }
}
