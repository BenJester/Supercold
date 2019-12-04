using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public bool active;
    public bool dead;
    public int maxHp;
    public int hp;
    public float speed;
    public Vector2 targetPos;
    public List<Buff> buffList;

    public float snapDistance = 10f;
    public bool canMove = true;

    public ThingParticle particle;

    CircleCollider2D col;
    Rigidbody2D body;
    SpriteRenderer sprite;

    public Action lastCastAction;

    void Start()
    {
        hp = maxHp;
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        targetPos = transform.position;
        particle = GetComponent<ThingParticle>();

        InitBuffs();
    }

    void InitBuffs()
    {
        foreach (var buff in buffList)
        {
            buff.Init();
        }
    }

    void FixedUpdate()
    {
        GoToTargetPos();
    }

    public void TakeDamage(int damage)
    {
        if (dead) return;
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        dead = true;
        //col.enabled = false;
        sprite.enabled = false;
    }

    public void AddBuff(Buff buff)
    {
        buffList.Add(buff);
        buff.Init();
        buff.Do();
        buff.owner = this;
    }

    void GoToTargetPos()
    {
        if (body.position == targetPos || !canMove) return;

        Vector2 dir = (targetPos - body.position).normalized;
        body.MovePosition(body.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(body.position, targetPos) <= snapDistance)
        {
            body.position = targetPos;
        }
    }

}
