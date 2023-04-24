using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/DirectionDashSkill")]
public class DirectionDashSkill : DirectionSkill
{
    public float dashSpeed;
    public float duration;
    public bool scanBullets;
    public float deflectBulletSpeed;
    Vector2 relativeDir;

    public override void Do()
    {

        relativeDir = (targetPos - owner.body.position).normalized;
        if (targetPos != null)
            Utility.Instance.StartCoroutine(Dash(targetPos));
    }

    IEnumerator Dash(Vector2 pos)
    {
        float timer = 0f;
        owner.canMove = false;

        while (timer < duration)
        {
            owner.body.MovePosition(owner.body.position + relativeDir * dashSpeed * Time.fixedDeltaTime);
            Scan();
            if (scanBullets)
                ScanBullets();
            yield return new WaitForEndOfFrame();
            timer += Time.fixedDeltaTime;
        }
        owner.canMove = true;
        owner.targetPos = targetPos;
    }

    void Scan()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(owner.transform.position, owner.col.radius * 4f, Utility.Instance.thingLayer);
        foreach (Collider2D col in cols)
        {
            Thing thing = col.GetComponent<Thing>();
            if (thing != null && thing.team != owner.team && !hitList.Contains(thing))
            {
                thing.TakeDamage(weaknessList, damage, owner);
                if (gainBuff != null)
                    thing.AddBuff(Instantiate(gainBuff));
                hitList.Add(thing);
            }
        }
    }

    void ScanBullets()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(owner.transform.position, owner.col.radius * 4f, Utility.Instance.bulletLayer);
        bool hit = false;
        foreach (Collider2D col in cols)
        {
            BulletBehavior bulletBehavior = col.GetComponent<BulletBehavior>();
            if (bulletBehavior != null && bulletBehavior.owner.team != owner.team)
            {
                bulletBehavior.bulletType = bulletType.Lock;
                bulletBehavior.travelSpeed = deflectBulletSpeed;
                bulletBehavior.target = bulletBehavior.owner.gameObject;
                bulletBehavior.owner = owner;
                if (!hit && gainBuff != null)
                {
                    owner.AddBuff(gainBuff);
                    hit = true;
                }
                    
            }
        }
    }
}
