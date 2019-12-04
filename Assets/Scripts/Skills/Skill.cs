using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Card, NotCard
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
    protected Thing thing;

    public abstract void Do();
    public abstract void OnKey();

    public void Cast()
    {
        if (isCard())
            Player.Instance.BroadcastPlayCard();
        Utility.Instance.StartCoroutine(CastTime());
    }

    IEnumerator CastTime()
    {
        PlayerParticle.Instance.PlayCastParticle();
        Player.Instance.canMove = false;
        OnCastFinish();
        yield return new WaitForSeconds(preCastTime);       
        Do();
        PlayerParticle.Instance.PauseCastParticle();
        yield return new WaitForSeconds(postCastTime);
        Player.Instance.canMove = true;
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
