using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ActivateSkill/Reload")]
public class Reload : ActivateSkill
{
    public bool discardHand;
    public bool drawToFull;
    public int drawNum;

    public override void Do()
    {
        if (discardHand)
            Player.Instance.ReturnHandToDeck();
        Player.Instance.RefillMana();
        if (drawToFull)
            Player.Instance.DrawCard(Player.Instance.handMaxNum);
        else
            Player.Instance.DrawCard(drawNum);
    }

}
