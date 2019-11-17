using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public bool active;

    public float travelSpeed;
    public int damage;
    public GameObject target;

    Rigidbody2D body;

    void Start()
    {

    }

    void Update()
    {
        Chase();
    }

    void Chase()
    {
        if (!active || !target) return;
        Vector2 dir = (target.transform.position - transform.position).normalized;
        transform.Translate(dir * travelSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Thing colThing = col.GetComponent<Thing>();
        if (colThing == target.GetComponent<Thing>())
        {
            OnHit(colThing);
        }
    }

    void OnHit(Thing thing)
    {
        thing.TakeDamage(damage);
        Destroy(gameObject);
    }
}
