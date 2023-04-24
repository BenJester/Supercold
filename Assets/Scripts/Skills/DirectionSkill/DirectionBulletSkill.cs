using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/DirectionBulletSkill")]
public class DirectionBulletSkill : DirectionSkill
{
    public float travelSpeed;
    public Sprite bulletSprite;

    public override void Do()
    {
        if (targetPos != null)
            Shoot(targetPos);
    }

    void Shoot(Vector2 pos)
    {
        GameObject bullet = Instantiate(Prefabs.Instance.bullet, owner.transform.position, Quaternion.identity, null);
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
        bulletBehavior.bulletType = bulletType.Direction;
        bulletBehavior.travelSpeed = travelSpeed;
        bulletBehavior.damage = damage;
        bulletBehavior.dir = pos;
        bulletBehavior.owner = owner;
        bulletBehavior.weaknessList = weaknessList;
    }
}
