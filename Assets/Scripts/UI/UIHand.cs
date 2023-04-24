using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHand : MonoBehaviour
{
    public List<UISkill> skillList;
    public UISkill reload;

    private void Update()
    {
        skillList[0].DisplaySkill(Player.Instance.QSkill);
        skillList[0].DisplayCooldown(Player.Instance.QSkillCooldown, Player.Instance.curMaxCooldown);
        skillList[1].DisplaySkill(Player.Instance.ESkill);
        skillList[1].DisplayCooldown(Player.Instance.ESkillCooldown, Player.Instance.curMaxCooldown);

        for (int i = 0; i < skillList.Count; i ++)
        {
            //skillList[i].DisplaySkill(Player.Instance.Hand[i]);
        }
        //reload.DisplaySkill(Player.Instance.Reload);
    }

    
}
