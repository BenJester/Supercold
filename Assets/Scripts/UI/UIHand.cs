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
            skillList[i].DisplaySkill(Player.Instance.Hand[i]);
        }
        reload.DisplaySkill(Player.Instance.Reload);
    }

    
}
