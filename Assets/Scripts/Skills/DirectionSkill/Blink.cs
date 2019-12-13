using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DirectionSkill/Blink")]
public class Blink : DirectionSkill
{
    public override void Do()
    {
        owner.transform.position = targetPos;
    }
}
