using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/LockOnSkill/DeflectSkill")]
public class DeflectSkill : LockOnSkill
{
    public float deflectBulletSpeed;
    public override void Do()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(owner.transform.position, range, Utility.Instance.bulletLayer);
        foreach (Collider2D col in cols)
        {
            BulletBehavior bulletBehavior = col.GetComponent<BulletBehavior>();
            if (bulletBehavior != null)
            {
                bulletBehavior.bulletType = bulletType.Lock;
                bulletBehavior.owner = owner;
                bulletBehavior.travelSpeed = deflectBulletSpeed;
                bulletBehavior.target = target;
            }
        }
    }
}
