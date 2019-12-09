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
        for (int i = 0; i < skillList.Count; i ++)
        {
            skillList[i].skillName.text = Player.Instance.Hand[i].skillName;
            skillList[i].detail.text = Player.Instance.Hand[i].detail;
            skillList[i].CastTime.text = (Player.Instance.Hand[i].preCastTime + Player.Instance.Hand[i].postCastTime).ToString("F2") + "s";
            skillList[i].skill = Player.Instance.Hand[i];
        }
        reload.skillName.text = Player.Instance.Reload.skillName;
        reload.detail.text = Player.Instance.Reload.detail;
        reload.CastTime.text = Player.Instance.Reload.preCastTime + Player.Instance.Reload.postCastTime + "s";
        reload.skill = Player.Instance.Reload;
    }
}
