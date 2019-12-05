using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public bool active;
    [HideInInspector] public Thing owner;
    [HideInInspector] public float travelSpeed;
    [HideInInspector] public int damage;
    public GameObject target;
    public Vector2 dir;
    Vector3 relativeDir;
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
        if (!active) return;
        if (relativeDir == Vector3.zero)
            relativeDir = ((Vector3)dir - transform.position).normalized;
        if (target)
        {
            Vector2 targetDir = (target.transform.position - transform.position).normalized;
            transform.Translate(targetDir * travelSpeed * Time.fixedDeltaTime);
        }
        else if (dir != null)
        {
            transform.Translate(relativeDir * travelSpeed * Time.fixedDeltaTime);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Thing colThing = col.GetComponent<Thing>();
        if (colThing == owner) return;
        if (target && colThing == target.GetComponent<Thing>())
        {
            OnHit(colThing);
        }
        else if (dir != null && colThing)
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
