using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 选择目标技能
[CreateAssetMenu (menuName = "Skills/LockOnSkill/BulletSkill")]
public class BulletSkill : LockOnSkill
{
    public float travelSpeed;
    public Sprite bulletSprite;
    public int damage;

    public override void Do()
    {
        if (target)
            Shoot(target);
    }

    void Shoot(GameObject target)
    {
        GameObject bullet = Instantiate(Prefabs.Instance.bullet, owner.transform.position, Quaternion.identity, null);
        BulletBehavior bulletBehavior =  bullet.GetComponent<BulletBehavior>();
        bulletBehavior.bulletType = bulletType.Lock;
        bulletBehavior.travelSpeed = travelSpeed;
        bulletBehavior.damage = damage;
        bulletBehavior.target = target;
        bulletBehavior.owner = owner;
    }
}
