using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : Skill
{
    public float travelSpeed;
    public Sprite bulletSprite;
    public int damage;

    public GameObject target;

    public override void Start()
    {
        base.Start();
        Shoot(target);
    }

    void Shoot(GameObject target)
    {
        GameObject bullet = Instantiate(Prefabs.Instance.bullet, transform.position, Quaternion.identity, null);
        BulletBehavior bulletBehavior =  bullet.GetComponent<BulletBehavior>();
        bulletBehavior.travelSpeed = travelSpeed;
        bulletBehavior.damage = damage;
        bulletBehavior.target = target;
    }
}
