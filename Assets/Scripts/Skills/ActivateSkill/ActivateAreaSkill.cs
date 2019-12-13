using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skills/ActivateSkill/AreaSkill")]
public class ActivateAreaSkill : ActivateSkill
{
    public float radius;
    public float delay;

    public override void Do()
    {
        GameObject area = Instantiate(Prefabs.Instance.area, owner.transform.position, Quaternion.identity, owner.transform);
        AreaBehavior areaBehavior = area.GetComponent<AreaBehavior>();
        areaBehavior.radius = radius;
        areaBehavior.damage = damage;
        areaBehavior.owner = owner;
        areaBehavior.delay = delay;
        areaBehavior.buff = gainBuff;
        areaBehavior.Init();
    }
}
