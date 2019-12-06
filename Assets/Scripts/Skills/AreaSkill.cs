using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkill : DirectionSkill
{
    public float radius;
    public float delay;
    public Sprite bulletSprite;
    public int damage;

    public override void Do()
    {
        if (targetPos != null)
            Shoot(targetPos);
    }

    void Shoot(Vector2 pos)
    {
        GameObject area = Instantiate(Prefabs.Instance.area, targetPos, Quaternion.identity, null);
        AreaBehavior areaBehavior = area.GetComponent<AreaBehavior>();

    }
}
