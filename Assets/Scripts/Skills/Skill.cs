using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Key
{
    None, Q, W, E, R, D, F
}

public abstract class Skill : ScriptableObject
{
    public string skillName = "New SKill";
    public Sprite sprite;
    public int cost;
    public float castTime;

    public bool active;
    public Key key;
    protected Rigidbody2D rb;
    protected Thing thing;

    public abstract void Init(GameObject obj);
    public abstract void Do();
    public abstract void OnKey();

    IEnumerator Cast()
    {
        yield return new WaitForSeconds(castTime);
    }

    // 检查是否可以执行
    public virtual bool Check()
    {
        return active;
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
