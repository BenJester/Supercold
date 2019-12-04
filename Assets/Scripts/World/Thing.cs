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

    public List<Buff> buffList;

    CircleCollider2D col;
    Rigidbody2D body;
    SpriteRenderer sprite;

    void Start()
    {
        hp = maxHp;
        col = GetComponent<CircleCollider2D>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
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
        buff.Do();
        buff.owner = this;
    }
}
