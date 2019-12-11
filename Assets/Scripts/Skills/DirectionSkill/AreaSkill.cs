using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/AreaSkill")]
public class AreaSkill : DirectionSkill
{
    public float radius;
    public float delay;
    public Sprite bulletSprite;

    public override void Do()
    {
        if (targetPos != null)
            Shoot(targetPos);
    }

    void Shoot(Vector2 pos)
    {
        GameObject area = Instantiate(Prefabs.Instance.area, targetPos, Quaternion.identity, null);
        AreaBehavior areaBehavior = area.GetComponent<AreaBehavior>();
        areaBehavior.radius = radius;
        areaBehavior.damage = damage;
        areaBehavior.owner = owner;
        areaBehavior.delay = delay;
        areaBehavior.buff = gainBuff;
        areaBehavior.Init();
    }
}
