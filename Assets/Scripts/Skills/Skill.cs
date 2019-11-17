using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Key
{
    None, Q, W, E, R, D, F
}

public class Skill : MonoBehaviour
{
    public bool active;
    public Key key;
    protected Rigidbody2D rb;
    protected Thing thing;

    public float castTime;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thing = GetComponent<Thing>();
        switch (key)
        {
            case Key.W:
                Player.Instance.wSkill = this;
                break;
            default:
                break;
        }

    }

    public virtual void OnKeyDown()
    {

    }

    public virtual void OnKey()
    {

    }

    public virtual void OnKeyUp()
    {

    }

    public virtual void Do()
    {
        
    }

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
