using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/LockOnSkill/Steal")]
public class StealSkill : LockOnSkill
{
    public float modifiedPreCastTime = 0f;
    public float modifiedPostCastTime = 0f;
    public override void Do()
    {
        EnemyAI ai = target.GetComponent<EnemyAI>();
        if (ai != null && owner == Player.Instance.thing)
        {
            Skill skill = Instantiate(ai.GetNextSkill());
            skill.preCastTime = modifiedPreCastTime;
            skill.postCastTime = modifiedPostCastTime;
            Player.Instance.DrawCard(skill);
        }
    }
}
