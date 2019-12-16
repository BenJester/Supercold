using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    OnLock, Activate, NotCard, Direction
}

public abstract class Skill : ScriptableObject
{
    public SkillType skillType;
    public string skillName = "New SKill";
    public string detail = "This is a card";
    public Sprite sprite;
    //public int cost;
    public float range;
    public float radius;
    public int damage;
    public Buff gainBuff;
    public float preCastTime;
    public float postCastTime;

    //public bool active;
    protected Rigidbody2D rb;
    [HideInInspector]
    public Thing owner;
    [HideInInspector]
    protected List<Thing> hitList = new List<Thing>();
    Action action;
    public abstract void Do();
    public abstract void OnKey();
    public abstract Action CreateAction();

    public void Cast()
    {
        action = CreateAction();
        OnCastFinish();
        if (action.skill.GetType() == typeof(Reload) && HasReloadInBuffer()) return;
        if (!owner.canCast)
        {
            owner.buffer.Enqueue(action);
            return;
        }
        Utility.Instance.StartCoroutine(CastTime());
    }
    
    bool HasReloadInBuffer()
    {
        foreach (var action in owner.buffer)
        {
            if (action.skill.GetType() == typeof(Reload))
                return true;
        }
        return false;
    }

    public IEnumerator CastTime()
    {
        while (owner.CheckStackableBool<StunnedBuff>())
            yield return new WaitForEndOfFrame();

        if (preCastTime > 0f)
            owner.particle.PlayCastParticle();
        
        owner.HPCanvas.ShowCastTimeBar(preCastTime + postCastTime);

        owner.canMove = false;
        owner.canCast = false;
        yield return new WaitForSeconds(preCastTime);
        
        owner.particle.PauseCastParticle();
        if (!owner.CheckStackableBool<StunnedBuff>())
        {
            Do();
            if (isCard())
            {
                owner.lastCastAction = action;
            }
            yield return new WaitForSeconds(postCastTime);
            owner.canMove = true;
            owner.canCast = true;
            
        }
        if (isCard() && owner == Player.Instance.thing)
            Player.Instance.BroadcastPlayCard(action);
        while (owner.CheckStackableBool<StunnedBuff>())
            yield return new WaitForEndOfFrame();
        
        owner.NextInBuffer();
    }

    public void OnCastFinish()
    {
        Player.Instance.DiscardCard(this);
    }

    public bool isCard()
    {
        return skillType != SkillType.NotCard;
    }

    public void Damage(Collider2D[] cols, int damage)
    {
        //foreach (var col in cols)
        //{
        //    Thing colThing = col.GetComponent<Thing>();
        //    if (colThing == thing)
        //        continue;
        //    colThing.Hit();
        //    colThing.hp -= damage;
        //}
    }
}
