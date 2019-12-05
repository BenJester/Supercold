using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ActivateSkill/Reload")]
public class Reload : ActivateSkill
{
    public override void Do()
    {
        Player.Instance.ReturnHandToDeck();
        Player.Instance.RefillMana();
        Player.Instance.DrawCard(Player.Instance.handMaxNum);
    }

}
