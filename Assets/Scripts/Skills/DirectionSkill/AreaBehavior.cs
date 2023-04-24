using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaBehavior : MonoBehaviour
{
    public bool active;
    bool activated;
    [HideInInspector] public Thing owner;
    [HideInInspector] public float delay;
    [HideInInspector] public float radius;
    [HideInInspector] public int damage;
    [HideInInspector] public List<WeaknessType> weaknessList;
    [HideInInspector] public Buff buff;
    float timer;
    public Text countdownText;
    public LayerMask layerMask;
    SpriteRenderer sprite;
    
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        timer = delay;
        sprite.size = new Vector2(radius * 2f, radius * 2f);
    }

    void Update()
    {
        timer -= Time.fixedDeltaTime;
        countdownText.text = timer.ToString("F2");
        if (timer <= 0f && !activated)
        {
            activated = true;
            timer = 0f;
            Do();
        }
    }

    void Do()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
        foreach (Collider2D col in cols)
        {
            Thing thing = col.GetComponent<Thing>();
            if (thing != null && thing.team != owner.team)
            {
                thing.TakeDamage(weaknessList, damage, owner);
                if (buff != null)
                    thing.AddBuff(Instantiate(buff));
            }
        }
        Destroy(gameObject);
    }
}
