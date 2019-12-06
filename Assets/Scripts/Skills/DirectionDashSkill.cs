using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/DirectionDashSkill")]
public class DirectionDashSkill : DirectionSkill
{
    public float dashSpeed;
    public float duration;
    public int damage;
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
            yield return new WaitForEndOfFrame();
            timer += Time.fixedDeltaTime;
        }
        owner.canMove = true;
        //owner.targetPos = owner.transform.position;
    }
}
