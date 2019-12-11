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
    public int damage;
    public Buff gainBuff;
    public float preCastTime;
    public float postCastTime;

    //public bool active;
    protected Rigidbody2D rb;
    [HideInInspector]
    public Thing owner;
    Action action;
    public abstract void Do();
    public abstract void OnKey();
    public abstract Action CreateAction();

    public void Cast()
    {

        action = CreateAction();
        OnCastFinish();
        if (!owner.canCast)
        {
            owner.buffer.Enqueue(action);

            return;
        }
        Utility.Instance.StartCoroutine(CastTime());
    }

    public IEnumerator CastTime()
    {
        if (isCard())
        {
            owner.lastCastAction = action;
        }

        if (preCastTime > 0f)
            owner.particle.PlayCastParticle();

        owner.HPCanvas.ShowCastTimeBar(preCastTime + postCastTime);

        owner.canMove = false;
        owner.canCast = false;
        yield return new WaitForSeconds(preCastTime);

        owner.particle.PauseCastParticle();
        if (!owner.stunned)
        {
            Do();
            yield return new WaitForSeconds(postCastTime);
            owner.canMove = true;
            owner.canCast = true;
            if (isCard() && owner == Player.Instance.thing)
                Player.Instance.BroadcastPlayCard(owner.lastCastAction);
        }
        
        while (owner.stunned)
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
