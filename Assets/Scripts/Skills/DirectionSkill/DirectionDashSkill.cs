using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/DirectionDashSkill")]
public class DirectionDashSkill : DirectionSkill
{
    public float dashSpeed;
    public float duration;
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
            yield return new WaitForEndOfFrame();
            timer += Time.fixedDeltaTime;
        }
        owner.canMove = true;
        owner.targetPos = targetPos;
    }

    void Scan()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(owner.transform.position, owner.col.radius, Utility.Instance.thingLayer);
        foreach (Collider2D col in cols)
        {
            Thing thing = col.GetComponent<Thing>();
            if (thing != null && thing.team != owner.team && !hitList.Contains(thing))
            {
                thing.TakeDamage(damage, owner);
                if (gainBuff != null)
                    thing.AddBuff(Instantiate(gainBuff));
                hitList.Add(thing);
            }
        }
    }
}
