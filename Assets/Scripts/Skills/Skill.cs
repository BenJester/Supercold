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
    public int cost;
    public float preCastTime;
    public float postCastTime;

    public bool active;
    protected Rigidbody2D rb;
    public Thing owner;

    public abstract void Do();
    public abstract void OnKey();
    public abstract Action CreateAction();

    public void Cast()
    {

        Action action = CreateAction();
        OnCastFinish();
        if (!owner.canMove)
        {
            owner.buffer.Enqueue(action);

            return;
        }
        
        if (isCard())
        {
            owner.lastCastAction = action;          
        }
            
        Utility.Instance.StartCoroutine(CastTime());
    }

    public IEnumerator CastTime()
    {
        if (preCastTime > 0f)
            owner.particle.PlayCastParticle();

        owner.canMove = false;
        
        yield return new WaitForSeconds(preCastTime);       
        Do();
        owner.particle.PauseCastParticle();
        yield return new WaitForSeconds(postCastTime);
        owner.canMove = true;

        if (isCard())
            Player.Instance.BroadcastPlayCard(owner.lastCastAction);

        owner.NextInBuffer();
    }

    public void OnCastFinish()
    {
        Player.Instance.DiscardCard(this);
    }


    // 检查是否可以执行
    public virtual bool Check()
    {
        return active;
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
