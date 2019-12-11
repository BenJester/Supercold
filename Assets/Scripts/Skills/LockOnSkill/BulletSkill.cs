using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 选择目标技能
[CreateAssetMenu (menuName = "Skills/LockOnSkill/BulletSkill")]
public class BulletSkill : LockOnSkill
{
    public float travelSpeed;
    public Sprite bulletSprite;
    public int num = 1;
    public float interval = 0.2f;
    public Buff gainBuff;

    public override void Do()
    {
        if (target)
            Utility.Instance.StartCoroutine(MultiShoot());
    }

    IEnumerator MultiShoot()
    {
        int count = 0;
        while (count < num)
        {
            count += 1;
            Shoot(target);
            yield return new WaitForSeconds(interval);
        }
        
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
        if (gainBuff != null)
            bulletBehavior.gainBuff = gainBuff;
    }
}
