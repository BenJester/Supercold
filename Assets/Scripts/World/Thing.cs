﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thing : MonoBehaviour
{
    public bool active;
    public bool dead;
    public int maxHp;
    public int hp;
    public int shield;

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
    public Queue<Action> buffer;

    Text hpText;

    void Start()
    {
        hp = maxHp;
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        targetPos = transform.position;
        particle = GetComponent<ThingParticle>();
        buffer = new Queue<Action>();
        InitBuffs();
        InitUI();
    }

    void InitBuffs()
    {
        foreach (var buff in buffList)
        {
            Buff newBuff = Instantiate(buff);
            newBuff.Init(this);
        }
    }

    void InitUI()
    {
        GameObject HPCanvas = Instantiate(Prefabs.Instance.HPCanvas, transform);
        HPCanvas.GetComponent<HPText>().thing = this;
    }

    void Update()
    {
        
    }

    public void NextInBuffer()
    {
        if (buffer.Count > 0 && canMove)
        {
            buffer.Dequeue().CastTimeDo();
        }
    }

    void FixedUpdate()
    {
        GoToTargetPos();
    }

    public void TakeDamage(int damage)
    {
        if (dead) return;
        if (shield > 0)
        {
            int dmgOnShield = Mathf.Min(shield, damage);
            shield -= dmgOnShield;
            damage -= dmgOnShield;
        }
            
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
        buff.Init(this);
        buff.Do();
    }

    void GoToTargetPos()
    {
        if (body.position == targetPos || !canMove) return;

        //buffer = new Queue<Action>();
        Vector2 dir = (targetPos - body.position).normalized;
        body.MovePosition(body.position + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(body.position, targetPos) <= snapDistance)
        {
            body.position = targetPos;
        }
    }

}
