using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ActivateSkill/ShieldSkill")]
public class ShieldSkill : ActivateSkill
{
    public int shieldAmount;

    public override void Do()
    {
        owner.shield += shieldAmount;
    }
}
