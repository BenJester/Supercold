using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ѡ��Ŀ�꼼��
[CreateAssetMenu(menuName = "Skills/LockOnSkill/AutoAttackSkill")]
public class AutoAttackSkill : LockOnSkill
{

    public override void Do()
    {
        if (target)
        {
            Thing targetThing = target.GetComponent<Thing>();
            if (targetThing != null)
            {
                targetThing.TakeDamage(weaknessList, damage, owner);
            }
        }
            
    }
}
