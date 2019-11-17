using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    public bool active;

    public int maxHp;
    public int hp;

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
        if (!active) return;
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        active = false;
        col.enabled = false;
        sprite.enabled = false;
    }
}
