using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum bulletType
{
    Lock, Direction
}

public class BulletBehavior : MonoBehaviour
{
    public bulletType bulletType;
    public bool active;
    [HideInInspector] public Thing owner;
    [HideInInspector] public float travelSpeed;
    [HideInInspector] public int damage;
    [HideInInspector] public Buff gainBuff;
    public GameObject target;
    public Vector2 dir;
    public Vector3 relativeDir;
    Rigidbody2D body;
    public Text countdownText;
    public LayerMask layerMask;

    void Start()
    {

    }

    void Update()
    {
        Check();
        
        HandleUI();
    }

    private void FixedUpdate()
    {
        Chase();
    }

    void Check()
    {
        if (owner.dead)
            Destroy(gameObject);
    }

    void HandleUI()
    {
        float countdown;
        switch (bulletType)
        {
            case bulletType.Lock:
                if (target == Player.Instance.gameObject)
                {
                    countdown = (Vector3.Distance(Player.Instance.transform.position, transform.position)
                               / travelSpeed);
                    countdownText.text = countdown.ToString("F2");
                }
                break;

            case bulletType.Direction:
                Thing hit = RaycastAlongTravel();
                if (hit != null && hit == Player.Instance.thing)
                {
                    //transform.Translate(targetDir * travelSpeed * Time.fixedDeltaTime);
                    countdown = (Vector3.Distance(Player.Instance.transform.position, transform.position)
                               / travelSpeed);
                    countdownText.text = countdown.ToString("F2");
                }
                else
                {
                    countdownText.text = "";
                }
                break;

            default:
                break;
        }
    }

    Thing RaycastAlongTravel()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, relativeDir, 10000f, layerMask);
        if (hit)
        {
            Thing thing = hit.transform.GetComponent<Thing>();
            if (thing != null && thing != owner)
                return thing;
            else
                return null;
        }
        else
            return null;
    }

    void Chase()
    {
        if (!active) return;

        if (relativeDir == Vector3.zero)
            relativeDir = ((Vector3)dir - transform.position).normalized;

        switch (bulletType)
        {
            case bulletType.Lock:
                Vector2 targetDir = (target.transform.position - transform.position).normalized;
                transform.Translate(targetDir * travelSpeed * Time.fixedDeltaTime);
                break;
            case bulletType.Direction:
                transform.Translate(relativeDir * travelSpeed * Time.fixedDeltaTime);
                break;
            default:
                break;
        }     
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Thing colThing = col.GetComponent<Thing>();
        if (colThing == null || colThing.team == owner.team) return;
        if (bulletType == bulletType.Lock && colThing == target.GetComponent<Thing>())
        {
            OnHit(colThing);
        }
        else if (bulletType == bulletType.Direction)
        {
            OnHit(colThing);
        }
    }

    void OnHit(Thing thing)
    {
        thing.TakeDamage(damage, owner);
        if (gainBuff != null)
            thing.AddBuff(Instantiate(gainBuff));
        Destroy(gameObject);
    }
}
